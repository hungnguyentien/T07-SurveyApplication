using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.BangKhaoSat.Validators
{
    public class UpdateBangKhaoSatDtoValidator : AbstractValidator<UpdateBangKhaoSatDto>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;

        public UpdateBangKhaoSatDtoValidator(IBangKhaoSatRepository bangKhaoSatRepository)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            Include(new IBangKhaoSatDtoValidator(_bangKhaoSatRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
