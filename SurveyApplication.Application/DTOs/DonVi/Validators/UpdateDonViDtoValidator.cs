using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DonVi.Validators;

public class UpdateDonViDtoValidator : AbstractValidator<UpdateDonViDto>
{
    private readonly IDonViRepository _donViRepository;

    public UpdateDonViDtoValidator(IDonViRepository donViRepository)
    {
        _donViRepository = donViRepository;
        Include(new IDonViDtoValidator(_donViRepository));

        RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}