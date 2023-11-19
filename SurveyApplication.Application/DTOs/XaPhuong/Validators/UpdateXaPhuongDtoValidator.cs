using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.XaPhuong.Validators;

public class UpdateXaPhuongDtoValidator : AbstractValidator<UpdateXaPhuongDto>
{
    private readonly IXaPhuongRepository _XaPhuongRepository;

    public UpdateXaPhuongDtoValidator(IXaPhuongRepository XaPhuongRepository)
    {
        _XaPhuongRepository = XaPhuongRepository;
        Include(new IXaPhuongDtoValidator(_XaPhuongRepository));

        //RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}