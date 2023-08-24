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
                .NotNull().NotEmpty().WithMessage("Mã bảng khảo sát không được để trống");

            RuleFor(p => p.MaDonVi)
                .MustAsync(async (maDonVi, token) =>
                {
                    var DonViViExists = await _donViRepository.ExistsByMaDonVi(maDonVi.ToString());
                    return !DonViViExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");
        }
    }
}