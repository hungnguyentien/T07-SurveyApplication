using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.TinhTp.Validators
{
    public class ITinhTpDtoValidator : AbstractValidator<ITinhTpDto>
    {
        private readonly ITinhTpRepository _TinhTpRepository;

        public ITinhTpDtoValidator(ITinhTpRepository TinhTpRepository)
        {
            _TinhTpRepository = TinhTpRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ParentCode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
