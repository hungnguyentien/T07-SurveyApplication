using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.BangKhaoSat.Validators
{
    public class IBangKhaoSatDtoValidator : AbstractValidator<IBangKhaoSatDto>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;

        public IBangKhaoSatDtoValidator(IBangKhaoSatRepository bangKhaoSatRepository)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;

            RuleFor(p => p.IdLoaiHinh)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.IdDotKhaoSat)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

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
