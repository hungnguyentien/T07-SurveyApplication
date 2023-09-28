using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Persistence.Repositories;

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
                }).WithMessage("Mã lĩnh vực đơn vị đã tồn tại!");

            RuleFor(p => p.TenLinhVuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(async (name, token) =>
                {
                    var nameExists = await _LinhVucHoatDongRepository.Exists(x => x.TenLinhVuc == name);
                    return !nameExists;
                }).WithMessage("Tên lĩnh vực đã tồn tại!");

            RuleFor(p => p.MoTa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}