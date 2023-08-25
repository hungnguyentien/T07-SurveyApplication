using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators
{
    
    public class UpdateDotKhaoSatDtoValitor : AbstractValidator<UpdateDotKhaoSatDto>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;

        public UpdateDotKhaoSatDtoValitor(IDotKhaoSatRepository dotKhaoSatRepository)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            Include(new IDotKhaoSatDtoValitor(_dotKhaoSatRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
