using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators;

public class UpdateDotKhaoSatDtoValidator : AbstractValidator<UpdateDotKhaoSatDto>
{
    private readonly IDotKhaoSatRepository _dotKhaoSatRepository;

    public UpdateDotKhaoSatDtoValidator(IDotKhaoSatRepository dotKhaoSatRepository)
    {
        _dotKhaoSatRepository = dotKhaoSatRepository;
        Include(new IDotKhaoSatDtoValidator(_dotKhaoSatRepository));

        RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}