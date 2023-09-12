using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Commands
{
    public class UpdateTinhTpCommand : IRequest<Unit>
    { 
        public UpdateTinhTpDto? TinhTpDto { get; set; }
    }
}
