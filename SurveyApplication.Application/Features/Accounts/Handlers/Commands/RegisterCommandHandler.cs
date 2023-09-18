using MediatR;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Domain.Models;
using SurveyApplication.Utility.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class RegisterCommandHandler : BaseMasterFeatures, IRequestHandler<RegisterCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(ISurveyRepositoryWrapper surveyRepository, UserManager<ApplicationUser> userManager) : base(
            surveyRepository)
        {
            _userManager = userManager;
        }

        public async Task<BaseCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
                throw new Exception($"Username '{request.UserName}' already exists.");

            var user = new ApplicationUser
            {
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                Name = request.Name,
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper(),
                EmailConfirmed = true,
                Address = request.Address
            };
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null) throw new Exception($"Email {request.Email} đã tồn tại!");
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                foreach (var roleName in request.LstRoleName)
                    await _userManager.AddToRoleAsync(user, roleName);

                if (request.MatrixPermission == null) return new BaseCommandResponse("Tạo mới thành công!");
                foreach (var claimModule in request.MatrixPermission)
                {
                    await _userManager.AddClaimAsync(user,
                        new Claim((claimModule.Module).ToString(),
                            JsonExtensions.SerializeToJson(claimModule.LstPermission),
                            JsonClaimValueTypes.JsonArray));
                }

                return new BaseCommandResponse("Tạo mới thành công!");
            }

            throw new Exception($"{result.Errors}");
        }
    }
}
