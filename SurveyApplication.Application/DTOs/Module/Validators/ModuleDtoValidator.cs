using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.Module.Validators;

public class ModuleDtoValidator : AbstractValidator<IModuleDto>
{
    private readonly IModuleRepository _moduleRepository;

    public ModuleDtoValidator(IModuleRepository moduleViRepository)
    {
        _moduleRepository = moduleViRepository;
        RuleFor(p => p.Name)
            .NotNull().NotEmpty().WithMessage("Tên Module không được để trống");

        RuleFor(p => p.Name)
            .MustAsync(async (tenModule, token) =>
            {
                var ModuleViExists = await _moduleRepository.ExistsByName(tenModule);
                return !ModuleViExists;
            }).WithMessage("Tên Module đã tồn tại!");

        RuleFor(p => p.RouterLink)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.CodeModule)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}