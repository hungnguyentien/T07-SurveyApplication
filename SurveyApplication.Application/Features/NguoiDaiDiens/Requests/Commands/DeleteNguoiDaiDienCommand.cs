using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands
{
    public class DeleteNguoiDaiDienCommand : IRequest
    {
        public int Id { get; set; }
    }
}
