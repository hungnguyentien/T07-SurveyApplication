using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class RoleCommandHandler : BaseMasterFeatures, IRequestHandler<RoleCommand, BaseCommandResponse>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleCommandHandler(ISurveyRepositoryWrapper surveyRepository, UserManager<ApplicationUser> userManager) : base(
            surveyRepository)
        {
            _userManager = userManager;
        }

        public async Task<BaseCommandResponse> Handle(RoleCommand request, CancellationToken cancellationToken)
        {
            var roleAdmin = new IdentityRole
            {
                Name = request.Name,
                NormalizedName = request.Name.ToUpper()
            };
            await _roleManager.CreateAsync(roleAdmin);
            foreach (var claimModule in request.MatrixPermission)
            {
                await _roleManager.AddClaimAsync(roleAdmin, new Claim((claimModule.Module).ToString(), JsonExtensions.SerializeToJson(claimModule.LstPermission), JsonClaimValueTypes.JsonArray));
            }

            return new BaseCommandResponse("Tạo mới thành công!");
        }
    }
}
