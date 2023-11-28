using MediatR;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands
{
    public class DeleteAccountCommand : IRequest<BaseCommandResponse>
    {
        public List<string> Ids { get; set; }
    }
}
