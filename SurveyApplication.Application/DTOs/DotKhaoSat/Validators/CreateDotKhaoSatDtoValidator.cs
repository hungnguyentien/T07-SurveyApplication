using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators;

public class CreateDotKhaoSatDtoValidator : AbstractValidator<CreateDotKhaoSatDto>
{
    private readonly IDotKhaoSatRepository _dotKhaoSatRepository;

    public CreateDotKhaoSatDtoValidator(IDotKhaoSatRepository dotKhaoSatRepository)
    {
        _dotKhaoSatRepository = dotKhaoSatRepository;
        Include(new DotKhaoSatDtoValidator(_dotKhaoSatRepository));
    }
}