using FluentValidation;

namespace SurveyApplication.Application.DTOs.CauHoi.Validators;

public class HangDtoValidator : AbstractValidator<HangDto>
{
    public HangDtoValidator()
    {
        RuleFor(p => p.MaHang)
            .NotNull().NotEmpty().WithMessage("Mã hàng không được để trống!");
        RuleFor(p => p.Noidung)
            .NotNull().NotEmpty().WithMessage("Nội dung không được để trống!");
    }
}