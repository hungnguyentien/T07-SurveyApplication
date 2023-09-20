using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Module.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Module.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Module.Handlers.Commands;

public class UpdateModuleCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateModuleCommand, Unit>
{
    private readonly IMapper _mapper;

    public UpdateModuleCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateModuleDtoValidator(_surveyRepo.Module);
        var validatorResult = await validator.ValidateAsync(request.ModuleDto);
        if (validatorResult.IsValid == false) throw new ValidationException(validatorResult);
        var Module = await _surveyRepo.Module.GetById(request.ModuleDto?.Id ?? 0);
        _mapper.Map(request.ModuleDto, Module);
        await _surveyRepo.Module.Update(Module);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}