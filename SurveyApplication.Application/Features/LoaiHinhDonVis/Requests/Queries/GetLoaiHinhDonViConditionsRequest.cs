using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries
{

    public class GetLoaiHinhDonViConditionsRequest : IRequest<PageCommandResponse<LoaiHinhDonViDto>>
    {
        public List<LoaiHinhDonViDto> Data { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }
    }
}
