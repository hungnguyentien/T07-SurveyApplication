using MediatR;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.DTOs.Common;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries
{
    public class GetUserConditionsRequest : BasePagingDto, IRequest<BaseQuerieResponse<AccountDto>>
    {
    }
}
