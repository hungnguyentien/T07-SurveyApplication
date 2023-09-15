using FluentValidation;

namespace SurveyApplication.Application.DTOs.CauHoi.Validators;

public class CotDtoValidator : AbstractValidator<CotDto>
{
    public CotDtoValidator()
    {
        RuleFor(p => p.MaCot)
            .NotNull().NotEmpty().WithMessage("Mã cột không được để trống!");
        RuleFor(p => p.Noidung)
            .NotNull().NotEmpty().WithMessage("Nội dung không được để trống!");
    }
}