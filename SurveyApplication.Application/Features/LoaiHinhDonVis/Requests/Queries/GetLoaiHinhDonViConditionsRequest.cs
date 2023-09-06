using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries
{

    public class GetLoaiHinhDonViConditionsRequest : IRequest<BaseQuerieResponse<LoaiHinhDonViDto>>
    {
        public List<LoaiHinhDonViDto> Data { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }

        public LoaiHinhDonViDto LoaiHinhDonViDto { get; set; }
    }
}
