using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDaiDien.Validators
{
    public class INguoiDaiDienDtoValidator : AbstractValidator<INguoiDaiDienDto>
    {
        private readonly INguoiDaiDienRepository _NguoiDaiDienRepository;

        public INguoiDaiDienDtoValidator(INguoiDaiDienRepository NguoiDaiDienRepository)
        {
            _NguoiDaiDienRepository = NguoiDaiDienRepository;

            RuleFor(p => p.HoTen)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SoDienThoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
