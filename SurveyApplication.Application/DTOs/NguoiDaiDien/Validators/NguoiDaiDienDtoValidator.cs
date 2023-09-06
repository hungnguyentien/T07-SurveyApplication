using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.NguoiDaiDien.Validators
{

    public class NguoiDaiDienDtoValidator : AbstractValidator<INguoiDaiDienDto>
    {
        private readonly INguoiDaiDienRepository _NguoiDaiDienRepository;
        public NguoiDaiDienDtoValidator(INguoiDaiDienRepository NguoiDaiDienViRepository)
        {
            _NguoiDaiDienRepository = NguoiDaiDienViRepository;

            //RuleFor(p => p.MaNguoiDaiDien)
            //    .NotNull().NotEmpty().WithMessage("Mã người đại diện không được để trống");

            //RuleFor(p => p.MaNguoiDaiDien)
            //    .MustAsync(async (maNguoiDaiDien, token) =>
            //    {
            //        var NguoiDaiDienViExists = await _NguoiDaiDienRepository.ExistsByMaNguoiDaiDien(maNguoiDaiDien);
            //        return !NguoiDaiDienViExists;
            //    }).WithMessage("Mã người đại diện đã tồn tại!");

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
}