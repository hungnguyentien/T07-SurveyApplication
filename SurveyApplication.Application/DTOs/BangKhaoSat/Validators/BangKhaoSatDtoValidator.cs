using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;

namespace SurveyApplication.Application.DTOs.BangKhaoSat.Validators
{
    
    public class BangKhaoSatDtoValidator : AbstractValidator<IBangKhaoSatDto>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;
        public BangKhaoSatDtoValidator(IBangKhaoSatRepository bangKhaoSatViRepository)
        {
            _bangKhaoSatRepository = bangKhaoSatViRepository;
            RuleFor(p => p.MaBangKhaoSat)
                .NotNull().NotEmpty().WithMessage("Mã bảng khảo sát không được để trống");

            RuleFor(p => p.MaBangKhaoSat)
                .MustAsync(async (maBangKhaoSat, token) =>
                {
                    var bangKhaoSatViExists = await _bangKhaoSatRepository.ExistsByMaBangKhaoSat(maBangKhaoSat);
                    return !bangKhaoSatViExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");

            RuleFor(p => p.MaLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.MaDotKhaoSat).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.TenBangKhaoSat)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.MoTa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NgayBatDau)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.NgayKetThuc)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}