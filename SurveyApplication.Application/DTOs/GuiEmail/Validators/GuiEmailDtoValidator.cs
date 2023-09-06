using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.GuiEmail.Validators
{
   
    public class GuiEmailDtoValidator : AbstractValidator<IGuiEmailDto>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;
        public GuiEmailDtoValidator(IGuiEmailRepository guiEmailRepository)
        {
            _guiEmailRepository = guiEmailRepository;

            RuleFor(p => p.MaGuiEmail)
                .NotNull().NotEmpty().WithMessage("Mã gửi email không được để trống");

            RuleFor(p => p.MaGuiEmail)
                .MustAsync(async (maGuiEmail, token) =>
                {
                    var guiEmailExists = await _guiEmailRepository.ExistsByMaGuiEmail(maGuiEmail); ;
                    return !guiEmailExists;
                }).WithMessage("Mã gửi email đã tồn tại!");

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
