using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Module.Requests.Commands;

public class DeleteModuleCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}