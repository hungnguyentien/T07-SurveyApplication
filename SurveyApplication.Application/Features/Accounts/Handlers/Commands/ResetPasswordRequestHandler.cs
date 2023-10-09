using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class ResetPasswordRequestHandler : BaseMasterFeatures, IRequestHandler<ResetPasswordRequest, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResetPasswordRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, IEmailSender emailSender, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor) : base(surveyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.ResetPasswordDto.Email);

            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, WebUtility.UrlDecode(request.ResetPasswordDto.Token), request.ResetPasswordDto.Password);
                if (!resetPassResult.Succeeded) throw new Exception($"Mật khẩu không khớp, mời nhập lại.");
            }
            return new BaseCommandResponse
            {
                Message = "Gửi thành công"
            };
        }
    }
}
