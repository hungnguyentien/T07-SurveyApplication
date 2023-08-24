using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class LoaiHinhDonViDtoValidator : AbstractValidator<LoaiHinhDonViDto>
    {
        public LoaiHinhDonViDtoValidator()
        {
            RuleFor(p => p.Tenloaihinh)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Mota)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
