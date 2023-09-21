using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.Module.Validators;

public class IModuleDtoValidator : AbstractValidator<IModuleDto>
{
    private readonly IModuleRepository _moduleRepository;

    public IModuleDtoValidator(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;

        RuleFor(p => p.RouterLink)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.CodeModule)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}