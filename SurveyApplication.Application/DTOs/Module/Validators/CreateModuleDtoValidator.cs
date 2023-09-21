using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.Module.Validators;

public class CreateModuleDtoValidator : AbstractValidator<CreateModuleDto>
{
    private readonly IModuleRepository _moduleRepository;

    public CreateModuleDtoValidator(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
        Include(new ModuleDtoValidator(_moduleRepository));
    }
}