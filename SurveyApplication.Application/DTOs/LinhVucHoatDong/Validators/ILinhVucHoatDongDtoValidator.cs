using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.LinhVucHoatDong.Validators
{
    public class ILinhVucHoatDongDtoValidator : AbstractValidator<ILinhVucHoatDongDto>
    {
        private readonly ILinhVucHoatDongRepository _LinhVucHoatDongRepository;

        public ILinhVucHoatDongDtoValidator(ILinhVucHoatDongRepository LinhVucHoatDongRepository)
        {
            _LinhVucHoatDongRepository = LinhVucHoatDongRepository;

            RuleFor(p => new { p.TenLinhVuc, p.MaLinhVuc })
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(async (model, token) =>
                {
                    var nameExists = await _LinhVucHoatDongRepository.Exists(x => x.TenLinhVuc == model.TenLinhVuc && x.MaLinhVuc != model.MaLinhVuc);
                    return !nameExists;
                }).WithMessage("Tên lĩnh vực đã tồn tại!");

            RuleFor(p => p.MoTa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
