using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.NguoiDung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDung.Validators
{
    

    public class INguoiDungDtoValitor : AbstractValidator<INguoiDungDto>
    {
        private readonly INguoiDungRepository _NguoiDungRepository;

        public INguoiDungDtoValitor(INguoiDungRepository NguoiDungRepository)
        {
            _NguoiDungRepository = NguoiDungRepository;

            RuleFor(p => p.DiaChi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.HoTen)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
