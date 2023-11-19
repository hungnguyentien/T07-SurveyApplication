using MediatR;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Role.Requests.Commands;

public class UpdateRoleCommand : IRequest<BaseCommandResponse>
{
    public UpdateRoleDto? UpdateRoleDto { get; set; }
}