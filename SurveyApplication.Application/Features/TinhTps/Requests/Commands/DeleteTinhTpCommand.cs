using MediatR;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Commands
{
    public class DeleteTinhTpCommand : IRequest<BaseCommandResponse>
    {
        public List<int> Ids { get; set; }
    }
}
