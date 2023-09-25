using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DonVi.Validators;

public class IDonViDtoValidator : AbstractValidator<IDonViDto>
{
    private readonly IDonViRepository _donViRepository;

    public IDonViDtoValidator(IDonViRepository donViRepository)
    {
        _donViRepository = donViRepository;

        RuleFor(p => p.IdLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");
        ;


        RuleFor(p => p.MaDonVi)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

        RuleFor(p => p.TenDonVi)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.DiaChi)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

       

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

    

        RuleFor(p => p.SoDienThoai)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}