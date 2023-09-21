using MediatR;
using SurveyApplication.Application.DTOs.Role;

namespace SurveyApplication.Application.Features.Role.Requests.Queries
{
    public class GetRoleListRequest : IRequest<List<RoleDto>>
    {
    }
}
