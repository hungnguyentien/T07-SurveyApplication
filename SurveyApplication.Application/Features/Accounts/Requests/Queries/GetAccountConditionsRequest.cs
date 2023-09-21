using MediatR;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.DTOs.Common;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries
{
    public class GetAccountConditionsRequest : BasePagingDto, IRequest<BaseQuerieResponse<AccountDto>>
    {
    }
}
