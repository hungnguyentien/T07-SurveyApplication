using MediatR;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Commands
{
    public class DeleteCauHoiCommand: IRequest<BaseCommandResponse>
    {
        public List<int> Ids { get; set; }
    }
}
