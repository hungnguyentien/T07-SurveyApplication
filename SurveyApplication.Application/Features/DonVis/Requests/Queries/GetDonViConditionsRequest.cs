using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Requests.Queries
{
    
    public class GetDonViConditionsRequest : IRequest<PageCommandResponse<DonViDto>>
    {
        public List<DonViDto> Data { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }
    }
}
