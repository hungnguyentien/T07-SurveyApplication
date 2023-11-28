using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using SurveyApplication.Infrastructure.Mail;
using SurveyApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Queries
{
    public class GetForgotPasswordRequestHandler : BaseMasterFeatures, IRequestHandler<GetForgotPasswordRequest, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private EmailSettings EmailSettings { get; }

        public GetForgotPasswordRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, IEmailSender emailSender, UserManager<ApplicationUser> userManager, IOptions<EmailSettings> emailSettings) : base(surveyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            EmailSettings = emailSettings.Value;

        }

        public async Task<BaseCommandResponse> Handle(GetForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new Exception($"Tài khoản {request.Email} không tồn tại.");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Tạo URL với token
            var callbackUrl = $"{EmailSettings.LinkDoiMatKhau}?email={user.Email}&token={WebUtility.UrlEncode(token)}";

            // Tạo nội dung email với đường dẫn URL
            var bodyEmail = $"Nhấn vào đường link sau để đặt lại mật khẩu của bạn: <a href='{callbackUrl}'>{callbackUrl}</a>";

            var resultSend = await _emailSender.SendEmail(bodyEmail, "Xác nhận đặt lại mật khẩu", user.Email);
            return new BaseCommandResponse
            {
                Message = "Gửi thành công"
            };
        }
    }
}
