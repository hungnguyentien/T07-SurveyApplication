using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.CauHoi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries
{
    public class GetConfigCauHoiRequest : IRequest<List<CauHoiDto>>
    {
        public int IdBangKhaoSat { get; set; }
    }
}
