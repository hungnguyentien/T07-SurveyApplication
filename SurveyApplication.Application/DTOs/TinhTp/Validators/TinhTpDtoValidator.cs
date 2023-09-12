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
    
    public class TinhTpDtoValidator : AbstractValidator<ITinhTpDto>
    {
        private readonly ITinhTpRepository _tinhTpRepository;
        public TinhTpDtoValidator(ITinhTpRepository tinhTpRepository)
        {
            _tinhTpRepository = tinhTpRepository;
            RuleFor(p => p.Code)

                .NotNull().NotEmpty().WithMessage("Mã code không được để trống");

            RuleFor(p => p.Code)
                .MustAsync(async (code, token) =>
                {
                    var tinhTpRepository = await _tinhTpRepository.Exists(x => x.Code == code);
                    return !tinhTpRepository;
                }).WithMessage("Mã code đã tồn tại!");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            
        }

       
    }
}
