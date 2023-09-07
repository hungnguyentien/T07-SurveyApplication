using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.PhieuKhaoSat.Validators
{
    public class KetQuaDtoValidator: AbstractValidator<IKetQuaDto>
    {
        private readonly IKetQuaRepository _ketQuaRepository;
        public KetQuaDtoValidator(IKetQuaRepository ketQuaRepository)
        {
            _ketQuaRepository = ketQuaRepository;
            RuleFor(p => p.Data)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .NotNull();
            RuleFor(p => p.IdGuiEmail).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");
        }
    }
}
