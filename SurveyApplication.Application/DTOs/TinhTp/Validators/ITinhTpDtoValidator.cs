using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.TinhTp.Validators;

public class ITinhTpDtoValidator : AbstractValidator<ITinhTpDto>
{
    private readonly ITinhTpRepository _TinhTpRepository;

    public ITinhTpDtoValidator(ITinhTpRepository TinhTpRepository)
    {
        _TinhTpRepository = TinhTpRepository;

        RuleFor(p => new { p.Name, p.Code })
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (model, token) =>
            {
                var nameExists =
                    await _TinhTpRepository.Exists(x => x.Name == model.Name && x.Code != model.Code && !x.Deleted);
                return !nameExists;
            }).WithMessage("Tên tỉnh thành phố đã tồn tại!");

        RuleFor(p => p.Type)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}