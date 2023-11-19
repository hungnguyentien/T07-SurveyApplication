using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;

public class DeleteXaPhuongCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}