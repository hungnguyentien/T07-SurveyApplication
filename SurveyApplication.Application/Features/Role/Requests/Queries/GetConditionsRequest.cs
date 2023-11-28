using MediatR;
using SurveyApplication.Application.DTOs.Common;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Application.DTOs.Role;

namespace SurveyApplication.Application.Features.Role.Requests.Queries
{
    public class GetConditionsRequest : BasePagingDto, IRequest<BaseQuerieResponse<RoleDto>>
    {
    }
}
