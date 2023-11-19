using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;

public class GetBangKhaoSatByDotKhaoSatRequest : IRequest<BaseQuerieResponse<BangKhaoSatDto>>
{
    public int Id { get; set; }
    public List<BangKhaoSatDto> Data { get; set; }
    public int PageCount { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? Keyword { get; set; }
}