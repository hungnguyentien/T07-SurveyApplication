using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands
{
    public class DeleteBangKhaoSatCommand : IRequest
    {
        public string MaBangKhaoSat { get; set; }
    }
}
