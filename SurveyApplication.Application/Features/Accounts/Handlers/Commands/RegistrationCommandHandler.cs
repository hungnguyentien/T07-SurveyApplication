using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Models;
using SurveyApplication.Application.DTOs.Account.Validators;
using SurveyApplication.Application.Exceptions;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Domain.Common;
using Microsoft.Extensions.Options;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class RegistrationCommandHandler : BaseMasterFeatures, IRequestHandler<RegistrationCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) : base(surveyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<BaseCommandResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateAccountDtoValidator(_surveyRepo.Account);
            var validatorResult = await validator.ValidateAsync(request.AccountDto);
            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }

            var account = _mapper.Map<ApplicationUser>(request.AccountDto);
            var hashedPassword = _userManager.PasswordHasher.HashPassword(account, request.AccountDto.Password);
            account.PasswordHash = hashedPassword;

            var result = await _userManager.CreateAsync(account);

            var addToRoleResult = await _userManager.AddToRoleAsync(account, request.RoleName);

            await _surveyRepo.SaveAync();
            response.Success = true;
            response.Message = "Tạo mới thành công";
            //response.Id = account.Id;
            return response;
        }
    }
}
