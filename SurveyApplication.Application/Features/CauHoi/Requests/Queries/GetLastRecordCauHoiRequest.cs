using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries
{
    public class GetLastRecordCauHoiRequest : IRequest<string>
    {
        public string? MaLoaiHinh { get; set; }
    }
}
