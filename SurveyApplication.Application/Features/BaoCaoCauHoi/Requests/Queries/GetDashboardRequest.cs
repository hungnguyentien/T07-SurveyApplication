using MediatR;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries
{
    public class GetDashBoardRequest : IRequest<DashBoardDto>
    {
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}
