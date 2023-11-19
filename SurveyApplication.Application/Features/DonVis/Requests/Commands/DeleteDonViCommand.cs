using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands;

public class DeleteDonViCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}