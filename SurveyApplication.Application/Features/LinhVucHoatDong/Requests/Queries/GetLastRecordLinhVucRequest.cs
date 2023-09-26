using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries
{
    public class GetLastRecordLinhVucRequest : IRequest<string>
    {
        public string? MaLinhVuc { get; set; }
    }
}
