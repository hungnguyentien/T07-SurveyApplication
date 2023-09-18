using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SurveyApplication.Application.Features.Role.Requests.Commands;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.Role.Handlers.Commands
{
    public class RoleCommandHandler : BaseMasterFeatures, IRequestHandler<RoleCommand, BaseCommandResponse>
    {
        private readonly RoleManager<Domain.Role> _roleManager;

        public RoleCommandHandler(ISurveyRepositoryWrapper surveyRepository, RoleManager<Domain.Role> roleManager) : base(
            surveyRepository)
        {
            _roleManager = roleManager;
        }

        public async Task<BaseCommandResponse> Handle(RoleCommand request, CancellationToken cancellationToken)
        {
            var roleAdmin = new Domain.Role
            {
                Name = request.CreateRoleDto.Name,
                NormalizedName = request.CreateRoleDto.Name.ToUpper(),
                ActiveFlag = (int)EnumCommon.ActiveFlag.Active,
                Deleted = false
            };
            await _roleManager.CreateAsync(roleAdmin);
            foreach (var claimModule in request.CreateRoleDto.MatrixPermission)
            {
                await _roleManager.AddClaimAsync(roleAdmin, new Claim(claimModule.Module.ToString(), JsonExtensions.SerializeToJson(claimModule.LstPermission.Select(x => x.Value)), JsonClaimValueTypes.JsonArray));
            }

            return new BaseCommandResponse("Tạo mới thành công!");
        }
    }
}
