using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;

public class GetDotKhaoSatListRequest : IRequest<List<DotKhaoSatDto>>
{
}