using FluentValidation;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.TinhTp.Validators
{
  
    public class UpdateTinhTpDtoValidator : AbstractValidator<UpdateTinhTpDto>
    {
        private readonly ITinhTpRepository _tinhTpRepository;

        public UpdateTinhTpDtoValidator(ITinhTpRepository tinhTpRepository)
        {
            _tinhTpRepository = tinhTpRepository;
            Include(new TinhTpDtoValidator(_tinhTpRepository));
            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
