using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.XaPhuong.Validators
{
    public class IXaPhuongDtoValidator : AbstractValidator<IXaPhuongDto>
    {
        private readonly IXaPhuongRepository _XaPhuongRepository;

        public IXaPhuongDtoValidator(IXaPhuongRepository XaPhuongRepository)
        {
            _XaPhuongRepository = XaPhuongRepository;

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
