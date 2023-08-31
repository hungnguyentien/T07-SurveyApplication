using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries
{
    public class GetDotKhaoSatConditionsRequest : IRequest<PageCommandResponse<DotKhaoSatDto>>
    {
        public List<DotKhaoSatDto> Data { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }
    }

}
