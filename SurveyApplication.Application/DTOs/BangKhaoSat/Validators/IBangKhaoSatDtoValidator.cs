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
