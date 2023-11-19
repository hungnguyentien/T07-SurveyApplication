using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.TinhTp.Validators;

public class TinhTpDtoValidator : AbstractValidator<ITinhTpDto>
{
    private readonly ITinhTpRepository _TinhTpRepository;

    public TinhTpDtoValidator(ITinhTpRepository TinhTpRepository)
    {
        _TinhTpRepository = TinhTpRepository;

        RuleFor(p => p.Code)
            .NotNull().NotEmpty().WithMessage("Mã code không được để trống");

        RuleFor(p => p.Code)
            .MustAsync(async (code, token) =>
            {
                var TinhTpExists = await _TinhTpRepository.ExistsByCode(code);
                return !TinhTpExists;
            }).WithMessage("Mã code đã tồn tại!");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (name, token) =>
            {
                var nameExists = await _TinhTpRepository.Exists(x => x.Name == name);
                return !nameExists;
            }).WithMessage("Tên tỉnh thành phố đã tồn tại!");

        RuleFor(p => p.Type)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}