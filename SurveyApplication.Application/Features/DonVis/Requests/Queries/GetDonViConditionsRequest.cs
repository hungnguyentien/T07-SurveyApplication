using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.DonVis.Requests.Queries;

public class GetDonViConditionsRequest : IRequest<BaseQuerieResponse<DonViDto>>
{
    public List<DonViDto> Data { get; set; }
    public int PageCount { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string Keyword { get; set; }

    public DonViDto DonViDto { get; set; }
}