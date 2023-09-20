using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.Module.Validators;

public class UpdateModuleDtoValidator : AbstractValidator<UpdateModuleDto>
{
    private readonly IModuleRepository _moduleRepository;

    public UpdateModuleDtoValidator(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
        Include(new IModuleDtoValidator(_moduleRepository));

        RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}