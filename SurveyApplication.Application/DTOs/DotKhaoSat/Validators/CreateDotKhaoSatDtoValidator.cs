using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators
{
    
    public class CreateDotKhaoSatDtoValidator : AbstractValidator<CreateDotKhaoSatDto>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;

        public CreateDotKhaoSatDtoValidator(IDotKhaoSatRepository dotKhaoSatRepository)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            Include(new DotKhaoSatDtoValidator(_dotKhaoSatRepository));
        }
    }
}
