using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries
{
    public class GetLastRecordDotKhaoSatRequest : IRequest<string>
    {
        public string? MaLoaiHinh { get; set; }
    }
}
