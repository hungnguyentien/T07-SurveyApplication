using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;

public class GetQuanHuyenConditionsRequest : IRequest<BaseQuerieResponse<QuanHuyenDto>>
{
    public List<QuanHuyenDto> Data { get; set; }
    public int PageCount { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string Keyword { get; set; }

    public QuanHuyenDto QuanHuyenDto { get; set; }
}