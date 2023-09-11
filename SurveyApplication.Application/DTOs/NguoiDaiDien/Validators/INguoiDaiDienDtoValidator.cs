using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.NguoiDaiDien.Validators;

public class INguoiDaiDienDtoValidator : AbstractValidator<INguoiDaiDienDto>
{
    private readonly INguoiDaiDienRepository _NguoiDaiDienRepository;

    public INguoiDaiDienDtoValidator(INguoiDaiDienRepository NguoiDaiDienRepository)
    {
        _NguoiDaiDienRepository = NguoiDaiDienRepository;

        RuleFor(p => p.IdDonVi).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

        RuleFor(p => p.HoTen)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.ChucVu)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.SoDienThoai)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        //RuleFor(p => p.MoTa)
        //    .NotEmpty().WithMessage("{PropertyName} is required.")
        //    .NotNull();
    }
}