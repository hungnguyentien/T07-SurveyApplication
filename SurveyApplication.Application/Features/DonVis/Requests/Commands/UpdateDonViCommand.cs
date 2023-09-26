using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands;

public class UpdateDonViCommand : IRequest<BaseCommandResponse>
{
    public UpdateDonViDto? DonViDto { get; set; }
}