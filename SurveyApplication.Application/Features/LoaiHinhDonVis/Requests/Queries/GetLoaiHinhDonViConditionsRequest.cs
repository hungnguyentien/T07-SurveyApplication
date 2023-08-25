using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries
{
    
    public class GetLoaiHinhDonViConditionsRequest : IRequest<List<LoaiHinhDonViDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Keyword { get; set; }
    }
}
