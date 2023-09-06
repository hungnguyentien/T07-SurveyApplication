using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DonVi.Validators
{
    public class CreateDonViDtoValidator : AbstractValidator<CreateDonViDto>
    {
        private readonly IDonViRepository _donViRepository;

        public CreateDonViDtoValidator(IDonViRepository donViRepository)
        {
            _donViRepository = donViRepository;
            Include(new DonViDtoValidator(_donViRepository));
        }
    }
}
