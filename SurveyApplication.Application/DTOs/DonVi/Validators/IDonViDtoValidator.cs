using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DonVi.Validators;

public class IDonViDtoValidator : AbstractValidator<IDonViDto>
{
    private readonly IDonViRepository _donViRepository;

    public IDonViDtoValidator(IDonViRepository donViRepository)
    {
        _donViRepository = donViRepository;

        RuleFor(p => p.IdLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

        RuleFor(p => p.MaDonVi)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

        RuleFor(p => new { p.TenDonVi, p.MaDonVi })
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (model, token) =>
            {
                var nameExists = await _donViRepository.Exists(x => x.TenDonVi == model.TenDonVi && x.MaDonVi != model.MaDonVi);
                return !nameExists;
            }).WithMessage("Tên đơn vị đã tồn tại!");

        RuleFor(p => p.DiaChi)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => new { p.Email, p.MaDonVi })
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (model, token) =>
            {
                var emailExists = await _donViRepository.Exists(x => x.Email == model.Email && x.MaDonVi != model.MaDonVi);
                return !emailExists;
            }).WithMessage("Email đã tồn tại!");

        RuleFor(p => p.SoDienThoai)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}