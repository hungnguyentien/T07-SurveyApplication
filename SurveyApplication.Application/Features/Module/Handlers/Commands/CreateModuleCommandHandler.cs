using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Module.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Module.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Module.Handlers.Commands;

public class CreateModuleCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateModuleCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateModuleCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateModuleCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateModuleDtoValidator(_surveyRepo.Module);
        var validatorResult = await validator.ValidateAsync(request.ModuleDto);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            throw new ValidationException(validatorResult);
        }

        var Module = _mapper.Map<Domain.Module>(request.ModuleDto);

        Module = await _surveyRepo.Module.Create(Module);
        await _surveyRepo.SaveAync();

        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = Module.Id;
        return response;
    }
}