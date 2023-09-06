using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators
{
    

    public class IDotKhaoSatDtoValidator : AbstractValidator<IDotKhaoSatDto>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;

        public IDotKhaoSatDtoValidator(IDotKhaoSatRepository dotKhaoSatRepository)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;

            RuleFor(p => p.IdLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.TenDotKhaoSat)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NgayBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NgayKetThuuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
