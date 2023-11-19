using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Module;
using SurveyApplication.Application.Features.Module.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Module.Handlers.Queries;

public class GetModuleConditionsRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetModuleConditionsRequest, BaseQuerieResponse<ModuleDto>>
{
    private readonly IMapper _mapper;

    public GetModuleConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseQuerieResponse<ModuleDto>> Handle(GetModuleConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var Modules = await _surveyRepo.Module.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize,
            x => (string.IsNullOrEmpty(request.Keyword) ||
                  (!string.IsNullOrEmpty(x.Name) && x.Name.Contains(request.Keyword))) && x.Deleted == false, "");
        var result = _mapper.Map<List<ModuleDto>>(Modules);
        return new BaseQuerieResponse<ModuleDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = Modules.TotalFilter,
            Data = result
        };
    }
}