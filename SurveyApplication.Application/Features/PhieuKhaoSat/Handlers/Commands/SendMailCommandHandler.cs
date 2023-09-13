using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands;

public class SendMailCommandHandler : BaseMasterFeatures, IRequestHandler<SendMailCommand, BaseCommandResponse>
{
    private readonly IEmailSender _emailSender;
    private EmailSettings EmailSettings { get; }
    public SendMailCommandHandler(ISurveyRepositoryWrapper surveyRepository, IEmailSender emailSender, IOptions<EmailSettings> emailSettings) : base(surveyRepository)
    {
        _emailSender = emailSender;
        EmailSettings = emailSettings.Value;
    }


    public async Task<BaseCommandResponse> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        var lstGuiEmail = await _surveyRepo.GuiEmail.GetAllListAsync(x => !x.Deleted && request.LstIdGuiMail.Contains(x.Id));
        if (!lstGuiEmail.Any())
            return new BaseCommandResponse
            {
                Message = "Gửi không thành công"
            };

        var pageSize = 10;
        var subscriberCount = lstGuiEmail.Count();
        var amountOfPages = (int)Math.Ceiling((double)subscriberCount / pageSize);
        for (var pageIndex = 0; pageIndex < amountOfPages; pageIndex++)
            await RunTasks(lstGuiEmail.Skip(pageIndex * pageSize).Take(pageSize).ToList(), request.IsThuHoi);

        return new BaseCommandResponse
        {
            Message = "Gửi thành công"
        };
    }

    private async Task RunTasks(IEnumerable<Domain.GuiEmail> lstGuiEmail, bool isThuHoi)
    {
        var tasks = lstGuiEmail.Select(guiEmail => Task.Run(() => DoWork(guiEmail, isThuHoi))).ToList();
        await Task.WhenAll(tasks);
    }

    private async Task DoWork(Domain.GuiEmail guiEmail, bool isThuHoi)
    {
        var thongTinChung = new EmailThongTinChungDto
        {
            IdGuiEmail = guiEmail.Id
        };
        var linkKhaoSat = isThuHoi ? "" : $"\n {EmailSettings.LinkKhaoSat}{StringUltils.EncryptWithKey(JsonConvert.SerializeObject(thongTinChung), EmailSettings.SecretKey)}";
        var bodyEmail = $"{guiEmail.NoiDung} {linkKhaoSat}";
        var resultSend = await _emailSender.SendEmail(bodyEmail, guiEmail.TieuDe, guiEmail.DiaChiNhan);
        guiEmail.TrangThai = resultSend.IsSuccess
            ? (int)EnumGuiEmail.TrangThai.ThanhCong
            : (int)EnumGuiEmail.TrangThai.GuiLoi;
        guiEmail.ThoiGian = DateTime.Now;
        await _surveyRepo.GuiEmail.UpdateAsync(guiEmail);
        await _surveyRepo.SaveAync();
    }
}