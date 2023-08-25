using MediatR;

using SurveyApplication.Application.DTOs.DotKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands
{
   
    public class UpdateDotKhaoSatCommand : IRequest<Unit>
    {
        public UpdateDotKhaoSatDto? DotKhaoSatDto { get; set; }
    }
}
