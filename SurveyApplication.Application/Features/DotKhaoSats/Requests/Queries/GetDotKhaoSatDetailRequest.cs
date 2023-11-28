using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;

public class GetDotKhaoSatDetailRequest : IRequest<DotKhaoSatDto>
{
    public int Id { get; set; }
}