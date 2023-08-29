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
                .NotNull().NotEmpty().WithMessage("Mã loại hình đơn vị không được để trống");

            RuleFor(p => p.MaLoaiHinh)
                .MustAsync(async (maLoaiHinh, token) =>
                {
                    var LoaiHinhDonViViExists = await _loaiHinhDonViRepository.ExistsByMaLoaiHinh(maLoaiHinh);
                    return !LoaiHinhDonViViExists;
                }).WithMessage("Mã loại hình đơn vị đã tồn tại!");

            RuleFor(p => p.TenLoaiHinh)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.MoTa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}