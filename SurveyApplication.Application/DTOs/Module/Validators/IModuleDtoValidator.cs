using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.Module.Validators;

public class IModuleDtoValidator : AbstractValidator<IModuleDto>
{
    private readonly IModuleRepository _moduleRepository;

    public IModuleDtoValidator(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;

        RuleFor(p => p.Name)
            .NotNull().NotEmpty().WithMessage("Tên Module không được để trống");

        RuleFor(p => new { p.Name, p.CodeModule })
            .MustAsync(async (model, token) =>
            {
                var ModuleViExists = await _moduleRepository.Exists(x => x.Name == model.Name && x.CodeModule != model.CodeModule && !x.Deleted);
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