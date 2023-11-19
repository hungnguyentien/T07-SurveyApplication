using MediatR;
using SurveyApplication.Application.DTOs.Role;

namespace SurveyApplication.Application.Features.Role.Requests.Queries;

public class GetRoleDetailRequest : IRequest<UpdateRoleDto>
{
    public string Id { get; set; }
}