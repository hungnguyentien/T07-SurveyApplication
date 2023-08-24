using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands
{
    public class CreateNguoiDaiDienCommand : IRequest<BaseCommandResponse>
    {
        //public CreateNguoiDaiDienDto? NguoiDaiDienDto { get; set; }
    }
}
