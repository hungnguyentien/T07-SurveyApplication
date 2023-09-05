using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Responses;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries
{
    public class GetCauHoiConditionsRequest : IRequest<BaseQuerieResponse<CauHoiDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }
    }
}
