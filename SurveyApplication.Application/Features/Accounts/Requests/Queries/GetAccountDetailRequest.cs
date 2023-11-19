using MediatR;
using SurveyApplication.Application.DTOs.Account;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries;

public class GetAccountDetailRequest : IRequest<UpdateAccountDto>
{
    public string Id { get; set; }
}