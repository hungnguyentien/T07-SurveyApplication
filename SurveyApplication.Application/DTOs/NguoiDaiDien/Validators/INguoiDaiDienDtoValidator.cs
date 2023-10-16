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

        RuleFor(p => new { p.Email, p.Id })
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (model, token) =>
            {
                var emailExists = await _NguoiDaiDienRepository.Exists(x => x.Email == model.Email && x.Id != model.Id && !x.Deleted);
                return !emailExists;
            }).WithMessage("Email người đại diện đã tồn tại!");

        //RuleFor(p => p.MoTa)
        //    .NotEmpty().WithMessage("{PropertyName} is required.")
        //    .NotNull();
    }
}