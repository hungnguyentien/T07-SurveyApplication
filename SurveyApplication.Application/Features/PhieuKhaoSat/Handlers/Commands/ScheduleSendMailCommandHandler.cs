using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using Microsoft.Extensions.Options;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Utility.LogUtils;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class ScheduleSendMailCommandHandler : BaseMasterFeatures, IRequestHandler<ScheduleSendMailCommand, BaseCommandResponse>
    {
        private readonly IEmailSender _emailSender;
        private readonly ILoggerManager _logger;
        private EmailSettings EmailSettings { get; }
        private const string PathTemplateEmail = @"TempData\EmailSender.html";
        private const string PathCv = @"TempData\cv";

        public ScheduleSendMailCommandHandler(ISurveyRepositoryWrapper surveyRepository,
            IEmailSender emailSender, IOptions<EmailSettings> emailSettings, ILoggerManager logger) : base(surveyRepository)
        {
            _emailSender = emailSender;
            EmailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<BaseCommandResponse> Handle(ScheduleSendMailCommand request, CancellationToken cancellationToken)
        {
            var lstGuiEmail = await _surveyRepo.GuiEmail.GetAllQueryable().AsNoTracking().Where(x => !x.Deleted && (x.TrangThai == (int)EnumGuiEmail.TrangThai.DangGui || x.TrangThai == (int)EnumGuiEmail.TrangThai.GuiLoi)).Take(10).ToListAsync(cancellationToken: cancellationToken);
            if (!lstGuiEmail.Any())
                return new BaseCommandResponse
                {
                    Message = "Gửi không thành công"
                };

            var lstDonVi = await _surveyRepo.DonVi.GetAllQueryable().AsNoTracking().Where(x => !x.Deleted && lstGuiEmail.Select(gm => gm.IdDonVi).Contains(x.Id)).Select(x => new { x.Id, x.MaDonVi }).ToListAsync(cancellationToken: cancellationToken);
            var path = Path.Combine(Directory.GetCurrentDirectory(), PathTemplateEmail);
            var externalHtmlContent = await File.ReadAllTextAsync(path, cancellationToken);
            var lstAttachment = new List<byte[]?>();
            var lstAttachmentName = new List<string>
            {
                "CV_1476TMĐT-KTS.pdf",
                "CV_56AITECH-KS.pdf"
            };
            foreach (var attachmentName in lstAttachmentName)
            {
                using var r = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @$"{PathCv}\{attachmentName}"));
                using var ms = new MemoryStream();
                await r.BaseStream.CopyToAsync(ms, cancellationToken);
                lstAttachment.Add(ms.ToArray());
            }

            foreach (var guiEmail in lstGuiEmail)
            {
                if (string.IsNullOrEmpty(guiEmail.NoiDung))
                {
                    var mdv = lstDonVi.FirstOrDefault(x => x.Id == guiEmail.IdDonVi)?.MaDonVi ?? "";
                    var bodyHtml = externalHtmlContent.Replace("{{LINK_SEND_MAIL}}", $"{EmailSettings.DomainKhaoSat}/khao-sat?maDoanhNghiep={mdv}");
                    guiEmail.NoiDung = bodyHtml;
                }
                else
                {
                    var thongTinChung = new EmailThongTinChungDto
                    {
                        IdGuiEmail = guiEmail.Id
                    };
                    guiEmail.NoiDung = $"{guiEmail.NoiDung} " +
                                        $"\n {EmailSettings.LinkKhaoSat}{StringUltils.EncryptWithKey(JsonConvert.SerializeObject(thongTinChung), EmailSettings.SecretKey)} " +
                                        $"\n Link khảo sát doanh nghiệp online: {EmailSettings.DomainKhaoSat}/khao-sat";
                }
            }

            const int pageSize = 5;
            var subscriberCount = lstGuiEmail.Count();
            var amountOfPages = (int)Math.Ceiling((double)subscriberCount / pageSize);
            for (var pageIndex = 0; pageIndex < amountOfPages; pageIndex++)
                await RunTasks(lstGuiEmail.Skip(pageIndex * pageSize).Take(pageSize).ToList(), lstAttachment, lstAttachmentName);

            return new BaseCommandResponse
            {
                Message = "Gửi thành công"
            };
        }

        private async Task RunTasks(IEnumerable<Domain.GuiEmail> lstGuiEmail, IList<byte[]?> lstAttachment, IList<string> lstAttachmentName)
        {
            var tasks = lstGuiEmail.Select(guiEmail => Task.Run(() => DoWork(guiEmail, lstAttachment, lstAttachmentName))).ToList();
            await Task.WhenAll(tasks);
        }

        private async Task DoWork(Domain.GuiEmail guiEmail, IList<byte[]?> lstAttachment, IList<string> lstAttachmentName)
        {
            var resultSend = await _emailSender.SendEmail(guiEmail.NoiDung, guiEmail.TieuDe, guiEmail.DiaChiNhan, lstAttachment, lstAttachmentName);
            if (!resultSend.IsSuccess)
                _logger.LogError($"----- Địa chỉ nhận {guiEmail.DiaChiNhan} \n {JsonConvert.SerializeObject(resultSend)}");

            guiEmail.TrangThai = resultSend.IsSuccess
                ? (int)EnumGuiEmail.TrangThai.ThanhCong
                : (int)EnumGuiEmail.TrangThai.GuiLoi;
            guiEmail.ThoiGian = DateTime.Now;
            await _surveyRepo.GuiEmail.UpdateAsync(guiEmail);
            await _surveyRepo.SaveAync();
        }
    }
}
