using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands
{
    public class CreateDotKhaoSatCommand : IRequest<BaseCommandResponse>
    {
        public CreateDotKhaoSatDto? DotKhaoSatDto { get; set; }
    }

}
