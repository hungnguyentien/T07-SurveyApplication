using FluentValidation;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.TinhTp.Validators
{
  
    public class ITinhTpDtoValidator : AbstractValidator<ITinhTpDto>
    {
        private readonly ITinhTpRepository _tinhTpRepository;

        public ITinhTpDtoValidator(ITinhTpRepository tinhTpRepository)
        {
            _tinhTpRepository = tinhTpRepository;

            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();


        }
       
    }
}
