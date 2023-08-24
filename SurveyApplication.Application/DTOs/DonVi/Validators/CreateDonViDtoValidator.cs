using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.DTOs.DonVi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
