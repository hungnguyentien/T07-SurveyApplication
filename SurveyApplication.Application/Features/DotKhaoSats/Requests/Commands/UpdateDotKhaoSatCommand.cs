using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;

public class UpdateDotKhaoSatCommand : IRequest<Unit>
{
    public UpdateDotKhaoSatDto? DotKhaoSatDto { get; set; }
}