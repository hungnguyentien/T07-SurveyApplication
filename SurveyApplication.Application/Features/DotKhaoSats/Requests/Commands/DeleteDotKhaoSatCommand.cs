using MediatR;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands
{
    public class DeleteDotKhaoSatCommand : IRequest
    {
        public int Id { get; set; }
    }
}
