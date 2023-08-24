using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands
{
    public class UpdateBangKhaoSatCommand : IRequest<Unit>
    { 
        public UpdateBangKhaoSatDto? BangKhaoSatDto { get; set; }
    }
}
