using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Commands;

public class DeleteTinhTpCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}