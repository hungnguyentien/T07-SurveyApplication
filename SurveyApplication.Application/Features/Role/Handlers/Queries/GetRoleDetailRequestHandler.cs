using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Application.Features.Role.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.Role.Handlers.Queries;

public class GetRoleDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetRoleDetailRequest, UpdateRoleDto>
{
    private readonly RoleManager<Domain.Role> _roleManager;

    public GetRoleDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, RoleManager<Domain.Role> roleManager)
        : base(surveyRepository)
    {
        _roleManager = roleManager;
    }

    public async Task<UpdateRoleDto> Handle(GetRoleDetailRequest request, CancellationToken cancellationToken)
    {
        var role = await _surveyRepo.Role.FirstOrDefaultAsync(x => !x.Deleted && x.Id == request.Id) ??
                   new Domain.Role();
        var rs = new UpdateRoleDto
        {
            Id = role.Id,
            Name = role.Name
        };
        if (string.IsNullOrEmpty(role.Id)) return rs;
        {
            var lstPermission = await _roleManager.GetClaimsAsync(role) ?? new List<Claim>();
            rs.MatrixPermission = lstPermission.Select(x => new MatrixPermission
            {
                Module = Convert.ToInt32(x.Type),
                NameModule = EnumUltils.GetDescriptionValue<EnumModule.Code>()
                    .GetValueOrDefault(Convert.ToInt32(x.Type), ""),
                LstPermission = JsonExtensions.DeserializeFromJson<List<int>>(x.Value).Select(v => new LstPermission
                {
                    Value = v,
                    Name = EnumUltils.GetDescriptionValue<EnumPermission.Type>().GetValueOrDefault(v, "")
                }).ToList()
            }).ToList();
        }

        return rs;
    }
}