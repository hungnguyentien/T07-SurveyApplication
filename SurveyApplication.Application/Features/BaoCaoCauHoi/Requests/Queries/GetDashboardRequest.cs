using MediatR;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.DTOs.BaoCaoDashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries
{
    public class GetDashboardRequest : IRequest<BaoCaoDashboardDto>
    {
        public int IdDotKhaoSat { get; set; }
        public int IdBangKhaoSat { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}
