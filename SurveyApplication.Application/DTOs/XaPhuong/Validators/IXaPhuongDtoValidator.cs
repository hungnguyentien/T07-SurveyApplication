using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Persistence.Repositories;

namespace SurveyApplication.Application.DTOs.XaPhuong.Validators
{
    public class IXaPhuongDtoValidator : AbstractValidator<IXaPhuongDto>
    {
        private readonly IXaPhuongRepository _XaPhuongRepository;

        public IXaPhuongDtoValidator(IXaPhuongRepository XaPhuongRepository)
        {
            _XaPhuongRepository = XaPhuongRepository;

            RuleFor(p => new { p.Name, p.Code })
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(async (model, token) =>
                {
                    var nameExists = await _XaPhuongRepository.Exists(x => x.Name == model.Name && x.Code != model.Code);
                    return !nameExists;
                }).WithMessage("Tên xã phường đã tồn tại!");

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ParentCode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
