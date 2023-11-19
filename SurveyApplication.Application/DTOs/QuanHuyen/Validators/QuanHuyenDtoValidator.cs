using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.QuanHuyen.Validators;

public class QuanHuyenDtoValidator : AbstractValidator<IQuanHuyenDto>
{
    private readonly IQuanHuyenRepository _quanHuyenRepository;

    public QuanHuyenDtoValidator(IQuanHuyenRepository quanHuyenViRepository)
    {
        _quanHuyenRepository = quanHuyenViRepository;

        RuleFor(p => p.Code)
            .NotNull().NotEmpty().WithMessage("Mã code không được để trống");

        RuleFor(p => p.Code)
            .MustAsync(async (code, token) =>
            {
                var quanHuyenExists = await _quanHuyenRepository.ExistsByCode(code);
                return !quanHuyenExists;
            }).WithMessage("Mã code đã tồn tại!");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (name, token) =>
            {
                var nameExists = await _quanHuyenRepository.Exists(x => x.Name == name);
                return !nameExists;
            }).WithMessage("Tên quận huyện đã tồn tại!");

        RuleFor(p => p.Type)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.ParentCode)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}