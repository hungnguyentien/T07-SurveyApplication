using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries
{
    public class GetThongTinChungRequest : IRequest<ThongTinChungDto>
    {
        public int IdGuiEmail { get; set; }
    }
}
