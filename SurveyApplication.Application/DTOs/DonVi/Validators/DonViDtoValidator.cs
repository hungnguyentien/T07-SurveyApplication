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
                .MustAsync(async (maDonVi, token) =>
                {
                    var DonViViExists = await _donViRepository.ExistsByMaDonVi(maDonVi);
                    return !DonViViExists;
                }).WithMessage("Mã đơn vị đã tồn tại!");

            RuleFor(p => p.IdLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.IdLinhVuc).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

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