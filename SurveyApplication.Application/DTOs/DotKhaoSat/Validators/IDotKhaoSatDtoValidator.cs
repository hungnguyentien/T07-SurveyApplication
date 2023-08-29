using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators
{
    

    public class IDotKhaoSatDtoValidator : AbstractValidator<IDotKhaoSatDto>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;

        public IDotKhaoSatDtoValidator(IDotKhaoSatRepository dotKhaoSatRepository)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;

            RuleFor(p => p.MaLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

            RuleFor(p => p.TenDotKhaoSat)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NgayBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NgayKetThuuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
