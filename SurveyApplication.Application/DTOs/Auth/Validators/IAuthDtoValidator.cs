using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.Auth.Validators
{
    public class IAuthDtoValidator : AbstractValidator<IAuthDto>
    {
        private readonly IAuthRepository _AuthRepository;

        public IAuthDtoValidator(IAuthRepository AuthRepository)
        {
            _AuthRepository = AuthRepository;

            RuleFor(p => p.DiaChi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.HoTen)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SoDienThoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
