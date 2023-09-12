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

            RuleFor(p => p.TenLinhVuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.MoTa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
