using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.Features.Role.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Role.Handlers.Commands;

public class UpdateRoleCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateRoleCommand, BaseCommandResponse>
{
    private readonly RoleManager<Domain.Role> _roleManager;

    public UpdateRoleCommandHandler(ISurveyRepositoryWrapper surveyRepository, RoleManager<Domain.Role> roleManager) :
        base(
            surveyRepository)
    {
        _roleManager = roleManager;
    }

    public async Task<BaseCommandResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.UpdateRoleDto.Id);

        if (role != null)
        {
            // Lấy danh sách quyền cũ của vai trò
            var existingClaims = await _roleManager.GetClaimsAsync(role);

            // Xóa tất cả các quyền cũ
            foreach (var claim in existingClaims) await _roleManager.RemoveClaimAsync(role, claim);

            // Thêm các quyền mới
            foreach (var claimModule in request.UpdateRoleDto.MatrixPermission)
                await _roleManager.AddClaimAsync(role,
                    new Claim(claimModule.Module.ToString(),
                        JsonExtensions.SerializeToJson(claimModule.LstPermission.Select(x => x.Value)),
                        JsonClaimValueTypes.JsonArray));
        }

        return new BaseCommandResponse("Sửa thành công!");
    }
}