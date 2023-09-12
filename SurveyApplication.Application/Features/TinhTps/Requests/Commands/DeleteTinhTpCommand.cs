using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Commands
{
    public class DeleteTinhTpCommand : IRequest
    {
        public int Id { get; set; }
    }
}
