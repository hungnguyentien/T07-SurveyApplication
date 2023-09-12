using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Queries
{

    public class GetXaPhuongConditionsRequest : IRequest<BaseQuerieResponse<XaPhuongDto>>
    {
        public List<XaPhuongDto> Data { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }

        public XaPhuongDto XaPhuongDto { get; set; }
    }
}
