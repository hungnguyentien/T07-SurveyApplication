using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Module;
using SurveyApplication.Application.Features.Module.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Module.Handlers.Queries;

public class GetModuleDetailRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetModuleDetailRequest, ModuleDto>
{
    private readonly IMapper _mapper;

    public GetModuleDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<ModuleDto> Handle(GetModuleDetailRequest request,
        CancellationToken cancellationToken)
    {
        var ModuleRepository = await _surveyRepo.Module.GetById(request.Id);
        return _mapper.Map<ModuleDto>(ModuleRepository);
    }
}