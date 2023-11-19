using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;

public class GetDotKhaoSatByLoaiHinhRequest : IRequest<List<DotKhaoSatDto>>
{
    public int Id { get; set; }
}