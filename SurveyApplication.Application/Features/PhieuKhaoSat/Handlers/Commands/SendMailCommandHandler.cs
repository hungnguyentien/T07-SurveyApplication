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

public class SendMailCommandHandler : IRequestHandler<SendMailCommand, EmailRespose>
{
    private readonly IEmailSender _emailSender;
    private EmailSettings EmailSettings { get; }
    private readonly IGuiEmailRepository _guiEmailRepository;

    public SendMailCommandHandler(IGuiEmailRepository guiEmailRepository,
        IEmailSender emailSender, IOptions<EmailSettings> emailSettings)
    {
        _guiEmailRepository = guiEmailRepository;
        _emailSender = emailSender;
        EmailSettings = emailSettings.Value;
    }


    public async Task<EmailRespose> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        var guiEmail = await _guiEmailRepository.FirstOrDefaultAsync(x =>
            x.Id == request.Id && x.ActiveFlag == (int)EnumCommon.ActiveFlag.Active && !x.Deleted);
        if (guiEmail == null) return new EmailRespose { IsSuccess = false, Message = "Không tìm thấy thông tin Email" };
        var thongTinChung = new EmailThongTinChungDto
        {
            IdGuiEmail = guiEmail.Id
        };
        var resultSend = await _emailSender.SendEmail(
            $"{guiEmail.NoiDung} \n {EmailSettings.LinkKhaoSat}{StringUltils.EncryptWithKey(JsonConvert.SerializeObject(thongTinChung), EmailSettings.SecretKey)}",
            guiEmail.TieuDe, guiEmail.DiaChiNhan);
        return resultSend;
    }
}