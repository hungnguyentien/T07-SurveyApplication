using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Application.Features.Role.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.Role.Handlers.Queries;

public class GetConditionsRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetConditionsRequest, BaseQuerieResponse<RoleDto>>
{
    private readonly IMapper _mapper;

    public GetConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseQuerieResponse<RoleDto>> Handle(GetConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var lstRole = await _surveyRepo.Role.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize,
            x => (string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword)) &&
                 x.ActiveFlag == (int)EnumCommon.ActiveFlag.Active && !x.Deleted, request.OrderBy ?? "");
        var result = _mapper.Map<List<RoleDto>>(lstRole);
        return new BaseQuerieResponse<RoleDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = lstRole.TotalFilter,
            Data = result
        };
    }
}