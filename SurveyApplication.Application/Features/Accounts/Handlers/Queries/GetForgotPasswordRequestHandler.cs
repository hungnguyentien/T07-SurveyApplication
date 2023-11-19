using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Queries;

public class GetForgotPasswordRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetForgotPasswordRequest, BaseCommandResponse>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetForgotPasswordRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper,
        IEmailSender emailSender, UserManager<ApplicationUser> userManager,
        IOptions<EmailSettings> emailSettings) : base(surveyRepository)
    {
        _mapper = mapper;
        _userManager = userManager;
        _emailSender = emailSender;
        EmailSettings = emailSettings.Value;
    }

    private EmailSettings EmailSettings { get; }

    public async Task<BaseCommandResponse> Handle(GetForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ??
                   throw new Exception($"Tài khoản {request.Email} không tồn tại.");
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Tạo URL với token
        var callbackUrl = $"{EmailSettings.LinkDoiMatKhau}?email={user.Email}&token={WebUtility.UrlEncode(token)}";

        // Tạo nội dung email với đường dẫn URL
        var bodyEmail =
            $"Nhấn vào đường link sau để đặt lại mật khẩu của bạn: <a href='{callbackUrl}'>{callbackUrl}</a>";

        var resultSend = await _emailSender.SendEmail(bodyEmail, "Xác nhận đặt lại mật khẩu", user.Email);
        return new BaseCommandResponse
        {
            Message = "Gửi thành công"
        };
    }
}