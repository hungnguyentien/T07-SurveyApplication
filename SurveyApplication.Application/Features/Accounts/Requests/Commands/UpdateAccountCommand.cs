using MediatR;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands
{
    public class UpdateAccountCommand : IRequest<BaseCommandResponse>
    {
        public UpdateAccountDto? AccountDto { get; set; }
    }
}
