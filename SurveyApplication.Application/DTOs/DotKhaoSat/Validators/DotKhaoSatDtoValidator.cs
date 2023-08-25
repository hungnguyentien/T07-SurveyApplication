using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators
{
   
    public class DotKhaoSatDtoValidator : AbstractValidator<IDotKhaoSatDto>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        public DotKhaoSatDtoValidator(IDotKhaoSatRepository dotKhaoSatViRepository)
        {
            _dotKhaoSatRepository = dotKhaoSatViRepository;
            RuleFor(p => p.MaDotKhaoSat)
                .NotNull().NotEmpty().WithMessage("Mã đợt khảo sát không được để trống");
            RuleFor(p => p.MaDotKhaoSat)
                .MustAsync(async (maDotKhaoSat, token) =>
                {
                    var dotKhaoSatExists = await _dotKhaoSatRepository.ExistsByMaDotKhaoSat(maDotKhaoSat);
                    return !dotKhaoSatExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");
        }
    }
}
