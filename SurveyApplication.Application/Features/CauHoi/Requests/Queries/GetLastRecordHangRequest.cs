using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries
{
    public class GetLastRecordHangRequest : IRequest<string>
    {
        public GenCauHoiDto GenCauHoiDto { get; set; }
    }
}
