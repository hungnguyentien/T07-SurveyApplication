using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Role.Requests.Commands;

public class DeleteRoleCommand : IRequest<BaseCommandResponse>
{
    public List<string> Ids { get; set; }
}