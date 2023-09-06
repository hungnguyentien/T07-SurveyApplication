using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.GuiEmail.Validators
{
    

    public class IGuiEmailDtoValidator : AbstractValidator<IGuiEmailDto>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;

        public IGuiEmailDtoValidator(IGuiEmailRepository guiEmailRepository)
        {
            _guiEmailRepository = guiEmailRepository;

            RuleFor(p => p.DiaChiNhan)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.TieuDe)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NoiDung)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
