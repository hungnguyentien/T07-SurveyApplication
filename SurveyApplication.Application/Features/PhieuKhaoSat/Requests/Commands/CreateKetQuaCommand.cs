using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands
{
    public class CreateKetQuaCommand : IRequest<BaseCommandResponse>
    {
        public CreateKetQuaDto CreateKetQuaDto { get; set; }
    }
}
