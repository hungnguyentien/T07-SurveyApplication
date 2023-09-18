using MediatR;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Role.Requests.Commands
{
    public class RoleCommand : IRequest<BaseCommandResponse>
    {
        public CreateRoleDto CreateRoleDto { get; set; }
    }
}
