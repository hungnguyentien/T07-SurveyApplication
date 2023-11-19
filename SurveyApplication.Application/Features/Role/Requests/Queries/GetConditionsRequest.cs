using MediatR;
using SurveyApplication.Application.DTOs.Common;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Role.Requests.Queries;

public class GetConditionsRequest : BasePagingDto, IRequest<BaseQuerieResponse<RoleDto>>
{
}