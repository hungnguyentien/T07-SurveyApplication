using MediatR;
using SurveyApplication.Application.DTOs.NguoiDung;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Auths.Requests.Commands
{
    public class RegisterCommand : IRequest<BaseCommandResponse>
    {
        public NguoiDungDto? NguoiDungDto { get; set; }
    }
}
