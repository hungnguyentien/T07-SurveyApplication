using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.GuiEmail.Validators;

public class UpdateGuiEmailDtoValidator : AbstractValidator<UpdateGuiEmailDto>
{
    private readonly IGuiEmailRepository _guiEmailRepository;

    public UpdateGuiEmailDtoValidator(IGuiEmailRepository guiEmailRepository)
    {
        _guiEmailRepository = guiEmailRepository;
        Include(new IGuiEmailDtoValidator(_guiEmailRepository));

        RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}