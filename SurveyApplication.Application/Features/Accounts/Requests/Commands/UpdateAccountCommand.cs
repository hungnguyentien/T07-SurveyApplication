using MediatR;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands
{
    public class UpdateAccountCommand : IRequest<BaseCommandResponse>
    {
        public UpdateAccountDto? AccountDto { get; set; }
    }
}
