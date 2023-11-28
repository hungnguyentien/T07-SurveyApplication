using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Module.Validators;
using SurveyApplication.Application.DTOs.Module.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Module.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Module.Handlers.Commands;

public class UpdateModuleCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateModuleCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateModuleCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateModuleDtoValidator(_surveyRepo.Module);
        var validatorResult = await validator.ValidateAsync(request.ModuleDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Cập nhật thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var Module = await _surveyRepo.Module.GetById(request.ModuleDto?.Id ?? 0);
        _mapper.Map(request.ModuleDto, Module);
        await _surveyRepo.Module.Update(Module);
        await _surveyRepo.SaveAync();

        response.Message = "Cập nhật thành công!";
        response.Id = Module?.Id ?? 0;
        return response;
    }
}