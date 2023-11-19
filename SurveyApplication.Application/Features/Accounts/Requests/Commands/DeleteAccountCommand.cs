using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands;

public class DeleteAccountCommand : IRequest<BaseCommandResponse>
{
    public List<string> Ids { get; set; }
}