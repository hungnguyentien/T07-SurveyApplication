using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands
{
    public class CreateBangKhaoSatCommand : IRequest<BaseCommandResponse>
    {
        public BangKhaoSatDto? BangKhaoSatDto { get; set; }
    }
}
