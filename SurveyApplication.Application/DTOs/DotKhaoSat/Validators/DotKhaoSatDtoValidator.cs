using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators
{
   
    public class DotKhaoSatDtoValidator : AbstractValidator<IDotKhaoSatDto>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        public DotKhaoSatDtoValidator(IDotKhaoSatRepository dotKhaoSatViRepository)
        {
            _dotKhaoSatRepository = dotKhaoSatViRepository;
            RuleFor(p => p.MaDotKhaoSat)
                .NotNull().NotEmpty().WithMessage("Mã đợt khảo sát không được để trống");

            RuleFor(p => p.MaDotKhaoSat)
                .MustAsync(async (maDotKhaoSat, token) =>
                {
                    var dotKhaoSatExists = await _dotKhaoSatRepository.ExistsByMaDotKhaoSat(maDotKhaoSat);
                    return !dotKhaoSatExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");

            RuleFor(p => p.IdLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.TenDotKhaoSat)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NgayBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must((rootObject, ngayBatDau) => BeAValidDate(ngayBatDau, rootObject.NgayKetThuc))
                .WithMessage("{PropertyName} must be a valid date and earlier than NgayKetThuc.");

            RuleFor(p => p.NgayKetThuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must((rootObject, ngayKetThuc) => BeAValidDate(rootObject.NgayBatDau, ngayKetThuc))
                .WithMessage("{PropertyName} must be a valid date and later than NgayBatDau.");
        }

        private bool BeAValidDate(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            if (ngayBatDau.HasValue && ngayKetThuc.HasValue)
            {
                return ngayBatDau <= ngayKetThuc;
            }
            return true;
        }
    }
}
