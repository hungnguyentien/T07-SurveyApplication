using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.QuanHuyen.Validators
{
    public class IQuanHuyenDtoValidator : AbstractValidator<IQuanHuyenDto>
    {
        private readonly IQuanHuyenRepository _QuanHuyenRepository;

        public IQuanHuyenDtoValidator(IQuanHuyenRepository QuanHuyenRepository)
        {
            _QuanHuyenRepository = QuanHuyenRepository;

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
