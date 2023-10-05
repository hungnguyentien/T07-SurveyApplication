using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DonVi.Validators
{

    public class DonViDtoValidator : AbstractValidator<IDonViDto>
    {
        private readonly IDonViRepository _donViRepository;
        public DonViDtoValidator(IDonViRepository donViViRepository)
        {
            _donViRepository = donViViRepository;
            RuleFor(p => p.MaDonVi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(p => p.MaDonVi)
                .MustAsync(async (maDonVi, token) =>
                {
                    var donViViExists = maDonVi != null && await _donViRepository.ExistsByMaDonVi(maDonVi);
                    return !donViViExists;
                }).WithMessage("Mã đơn vị đã tồn tại!");

            RuleFor(p => p.IdLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.TenDonVi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(async (name, token) =>
                {
                    var nameExists = await _donViRepository.Exists(x => x.TenDonVi == name);
                    return !nameExists;
                }).WithMessage("Tên đơn vị đã tồn tại!");

            RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (email, token) =>
            {
                var emailExists = await _donViRepository.ExistsByEmail(email);
                return !emailExists;
            }).WithMessage("Email đơn vị đã tồn tại!");

            RuleFor(p => p.SoDienThoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.IdTinhTp)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.IdQuanHuyen)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.IdXaPhuong)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        }
    }
}