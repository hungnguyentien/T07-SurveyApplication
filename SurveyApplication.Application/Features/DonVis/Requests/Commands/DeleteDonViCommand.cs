using MediatR;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands;

public class DeleteDonViCommand : IRequest
{
    public int Id { get; set; }
}