using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Queries
{

    public class GetTinhTpConditionsRequest : IRequest<BaseQuerieResponse<TinhTpDto>>
    {
        public List<TinhTpDto> Data { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }

        public TinhTpDto TinhTpDto { get; set; }
    }
}
