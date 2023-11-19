using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.TinhTp.Validators;

public class UpdateTinhTpDtoValidator : AbstractValidator<UpdateTinhTpDto>
{
    private readonly ITinhTpRepository _TinhTpRepository;

    public UpdateTinhTpDtoValidator(ITinhTpRepository TinhTpRepository)
    {
        _TinhTpRepository = TinhTpRepository;
        Include(new ITinhTpDtoValidator(_TinhTpRepository));

        //RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}