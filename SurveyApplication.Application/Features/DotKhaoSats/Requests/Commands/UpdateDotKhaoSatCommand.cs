using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;

public class UpdateDotKhaoSatCommand : IRequest<BaseCommandResponse>
{
    public UpdateDotKhaoSatDto? DotKhaoSatDto { get; set; }
}