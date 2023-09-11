using MediatR;
using SurveyApplication.Application.DTOs.DonVi;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands;

public class UpdateDonViCommand : IRequest<Unit>
{
    public UpdateDonViDto? DonViDto { get; set; }
}