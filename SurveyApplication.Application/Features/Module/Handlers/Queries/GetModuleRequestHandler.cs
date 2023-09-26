using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Module;
using SurveyApplication.Application.Features.Module.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Module.Handlers.Queries;

public class GetModuleListRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetModuleListRequest, List<ModuleDto>>
{
    private readonly IMapper _mapper;

    public GetModuleListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<ModuleDto>> Handle(GetModuleListRequest request,
        CancellationToken cancellationToken)
    {
        var Modules = await _surveyRepo.Module.GetAllListAsync(x => !x.Deleted);
        return _mapper.Map<List<ModuleDto>>(Modules);
    }
}