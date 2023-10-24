using MediatR;
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
        private EmailSettings EmailSettings { get; }
        private readonly ILoggerManager _logger;

        public ScheduleSendMailCommandHandler(ISurveyRepositoryWrapper surveyRepository,
            IEmailSender emailSender, IOptions<EmailSettings> emailSettings, ILoggerManager logger) : base(surveyRepository)
        {
            _emailSender = emailSender;
            EmailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<BaseCommandResponse> Handle(ScheduleSendMailCommand request, CancellationToken cancellationToken)
        {
            var lstGuiEmail = await _surveyRepo.GuiEmail.GetAllListAsync(x => !x.Deleted && (x.TrangThai == (int)EnumGuiEmail.TrangThai.DangGui || x.TrangThai == (int)EnumGuiEmail.TrangThai.GuiLoi));
            if (!lstGuiEmail.Any())
                return new BaseCommandResponse
                {
                    Message = "Gửi không thành công"
                };

            var pageSize = 10;
            var subscriberCount = lstGuiEmail.Count();
            var amountOfPages = (int)Math.Ceiling((double)subscriberCount / pageSize);
            for (var pageIndex = 0; pageIndex < amountOfPages; pageIndex++)
                await RunTasks(lstGuiEmail.Skip(pageIndex * pageSize).Take(pageSize).ToList());

            return new BaseCommandResponse
            {
                Message = "Gửi thành công"
            };
        }

        private async Task RunTasks(IEnumerable<Domain.GuiEmail> lstGuiEmail)
        {
            var tasks = lstGuiEmail.Select(guiEmail => Task.Run(() => DoWork(guiEmail))).ToList();
            await Task.WhenAll(tasks);
        }

        private async Task DoWork(Domain.GuiEmail guiEmail)
        {
            var thongTinChung = new EmailThongTinChungDto
            {
                IdGuiEmail = guiEmail.Id
            };
            var bodyEmail =
                $"{guiEmail.NoiDung} \n {EmailSettings.LinkKhaoSat}{StringUltils.EncryptWithKey(JsonConvert.SerializeObject(thongTinChung), EmailSettings.SecretKey)}";
            var resultSend = await _emailSender.SendEmail(bodyEmail, guiEmail.TieuDe, guiEmail.DiaChiNhan);
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
