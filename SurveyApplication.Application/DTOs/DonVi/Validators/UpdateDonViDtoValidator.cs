using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DonVi.Validators
{
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
}
