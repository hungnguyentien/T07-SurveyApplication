using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.XaPhuong.Validators;

public class XaPhuongDtoValidator : AbstractValidator<IXaPhuongDto>
{
    private readonly IXaPhuongRepository _xaPhuongRepository;

    public XaPhuongDtoValidator(IXaPhuongRepository xaPhuongRepository)
    {
        _xaPhuongRepository = xaPhuongRepository;

        RuleFor(p => p.Code)
            .NotNull().NotEmpty().WithMessage("Mã code không được để trống");

        RuleFor(p => p.Code)
            .MustAsync(async (code, token) =>
            {
                var xaPhuongExists = await _xaPhuongRepository.ExistsByCode(code);
                return !xaPhuongExists;
            }).WithMessage("Mã code đã tồn tại!");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (name, token) =>
            {
                var nameExists = await _xaPhuongRepository.Exists(x => x.Name == name);
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