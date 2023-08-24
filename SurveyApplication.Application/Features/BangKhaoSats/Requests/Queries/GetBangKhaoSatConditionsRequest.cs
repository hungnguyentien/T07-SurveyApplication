using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries
{
    
    public class GetBangKhaoSatConditionsRequest : IRequest<List<BangKhaoSatDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Keyword { get; set; }
    }
}
