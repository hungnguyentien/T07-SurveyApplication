using MediatR;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Application.DTOs.Account;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands
{
    public class RegisterCommand : IRequest<BaseCommandResponse>
    {
        public RegisterDto Register { get; set; }
    }
}
