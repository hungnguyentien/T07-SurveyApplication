using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetForgotPasswordRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, IEmailSender emailSender, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor) : base(surveyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(GetForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new Exception($"Tài khoản {request.Email} không tồn tại.");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Sử dụng IHttpContextAccessor để truy cập HttpContext
            var origin = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            // Tạo URL với token
            var callbackUrl = $"{origin}/Account/ResetPassword?userId={user.Email}&token={token}";

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
