using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;

public class GetBangKhaoSatListRequest : IRequest<List<BangKhaoSatDto>>
{
}