using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;

namespace SurveyApplication.Application.DTOs.NguoiDaiDien.Validators
{
    
    public class NguoiDaiDienDtoValidator : AbstractValidator<INguoiDaiDienDto>
    {
        private readonly INguoiDaiDienRepository _NguoiDaiDienRepository;
        public NguoiDaiDienDtoValidator(INguoiDaiDienRepository NguoiDaiDienViRepository)
        {
            _NguoiDaiDienRepository = NguoiDaiDienViRepository;

            RuleFor(p => p.MaNguoiDaiDien)
                .NotNull().NotEmpty().WithMessage("Mã bảng khảo sát không được để trống");

            RuleFor(p => p.MaNguoiDaiDien)
                .MustAsync(async (maNguoiDaiDien, token) =>
                {
                    var NguoiDaiDienViExists = await _NguoiDaiDienRepository.ExistsByMaNguoiDaiDien(maNguoiDaiDien.ToString());
                    return !NguoiDaiDienViExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");
        }
    }
}