using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries;

public class GetCauHoiConditionsRequest : IRequest<BaseQuerieResponse<CauHoiDto>>
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? Keyword { get; set; }
    public string? OrderBy { get; set; }
}