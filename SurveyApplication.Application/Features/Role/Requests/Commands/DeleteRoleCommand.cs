using MediatR;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Role.Requests.Commands
{
    public class DeleteRoleCommand : IRequest<BaseCommandResponse>
    {
        public List<string> Ids { get; set; }
    }
}
