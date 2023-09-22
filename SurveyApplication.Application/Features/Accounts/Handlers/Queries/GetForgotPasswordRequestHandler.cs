using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using SurveyApplication.Infrastructure.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Queries
{
    public class GetForgotPasswordRequestHandler : BaseMasterFeatures, IRequestHandler<GetForgotPasswordRequest, ForgotPasswordDto>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetForgotPasswordRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, IEmailSender emailSender, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : base(surveyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<ForgotPasswordDto> Handle(GetForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new Exception($"Tài khoản {request.Email} không tồn tại.");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            //var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);

            //var accountRepository = await _emailSender.SendEmailAsync();
            var accountRepository = await _surveyRepo.Account.GetAsync(request.Email);

            return _mapper.Map<ForgotPasswordDto>(accountRepository);
        }
    }
}
