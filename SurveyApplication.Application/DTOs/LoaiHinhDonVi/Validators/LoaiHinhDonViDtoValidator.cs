using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    
    public class LoaiHinhDonViDtoValidator : AbstractValidator<ILoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        public LoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository loaiHinhDonViViRepository)
        {
            _loaiHinhDonViRepository = loaiHinhDonViViRepository;
            RuleFor(p => p.MaLoaiHinh)
                .NotNull().NotEmpty().WithMessage("Mã bảng khảo sát không được để trống");
            RuleFor(p => p.MaLoaiHinh)
                .MustAsync(async (maLoaiHinh, token) =>
                {
                    var LoaiHinhDonViViExists = await _loaiHinhDonViRepository.ExistsByMaLoaiHinh(maLoaiHinh);
                    return !LoaiHinhDonViViExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");
        }
    }
}