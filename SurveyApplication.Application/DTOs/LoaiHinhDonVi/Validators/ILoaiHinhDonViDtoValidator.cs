using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;

public class ILoaiHinhDonViDtoValidator : AbstractValidator<ILoaiHinhDonViDto>
{
    private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;

    public ILoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository LoaiHinhDonViRepository)
    {
        _LoaiHinhDonViRepository = LoaiHinhDonViRepository;

        RuleFor(p => p.TenLoaiHinh)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.MoTa)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}