using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries
{
    public class GetIpAddressRequest : IRequest<BaseQuerieResponse<IpAddressDto>>
    {
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; } = "";
    }
}
