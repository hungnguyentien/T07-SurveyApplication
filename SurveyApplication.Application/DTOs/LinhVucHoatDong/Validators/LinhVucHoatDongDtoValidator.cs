using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.LinhVucHoatDong.Validators
{
    
    public class LinhVucHoatDongDtoValidator : AbstractValidator<ILinhVucHoatDongDto>
    {
        private readonly ILinhVucHoatDongRepository _LinhVucHoatDongRepository;
        public LinhVucHoatDongDtoValidator(ILinhVucHoatDongRepository LinhVucHoatDongViRepository)
        {
            _LinhVucHoatDongRepository = LinhVucHoatDongViRepository;
            RuleFor(p => p.MaLinhVuc)
                .NotNull().NotEmpty().WithMessage("Mã lĩnh vực hoạt động không được để trống");

            RuleFor(p => p.MaLinhVuc)
                .MustAsync(async (maLoaiHinh, token) =>
                {
                    var LinhVucHoatDongViExists = await _LinhVucHoatDongRepository.ExistsByMaLinhVuc(maLoaiHinh);
                    return !LinhVucHoatDongViExists;
                }).WithMessage("Mã lĩnh vực đã hoạt động tồn tại!");

            RuleFor(p => p.TenLinhVuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.MoTa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}