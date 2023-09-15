using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands;

public class CreateDonViCommand : IRequest<BaseCommandResponse>
{
    public CreateDonViDto? DonViDto { get; set; }
}