using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;

namespace SurveyApplication.Application.DTOs.DonVi.Validators
{
    
    public class DonViDtoValidator : AbstractValidator<IDonViDto>
    {
        private readonly IDonViRepository _donViRepository;
        public DonViDtoValidator(IDonViRepository donViViRepository)
        {
            _donViRepository = donViViRepository;

            RuleFor(p => p.MaDonVi)
                .NotNull().NotEmpty().WithMessage("Mã đơn vị không được để trống");

            RuleFor(p => p.MaDonVi)
                .MustAsync(async (maDonVi, token) =>
                {
                    var DonViViExists = await _donViRepository.ExistsByMaDonVi(maDonVi.ToString());
                    return !DonViViExists;
                }).WithMessage("Mã đơn vị đã tồn tại!");

            RuleFor(p => p.MaLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.MaLinhVuc).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.TenDonVi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.DiaChi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.MaSoThue)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.WebSite)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SoDienThoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}