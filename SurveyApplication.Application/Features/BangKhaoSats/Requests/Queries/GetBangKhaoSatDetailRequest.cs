using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;

public class GetBangKhaoSatDetailRequest : IRequest<BangKhaoSatDto>
{
    public int Id { get; set; }
}