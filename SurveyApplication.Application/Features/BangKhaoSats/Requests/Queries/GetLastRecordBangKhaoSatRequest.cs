using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries
{
    public class GetLastRecordBangKhaoSatRequest : IRequest<string>
    {
        public string? MaLoaiHinh { get; set; }
    }
}
