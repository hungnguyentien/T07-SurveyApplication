using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Commands
{
    public class DeleteQuanHuyenCommand : IRequest
    {
        public int Id { get; set; }
    }
}
