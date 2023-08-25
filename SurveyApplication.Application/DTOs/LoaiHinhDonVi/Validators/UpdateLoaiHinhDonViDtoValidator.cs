using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class UpdateLoaiHinhDonViDtoValidator : AbstractValidator<UpdateLoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;

        public UpdateLoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository LoaiHinhDonViRepository)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            Include(new ILoaiHinhDonViDtoValidator(_LoaiHinhDonViRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
