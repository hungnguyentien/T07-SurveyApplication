using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DonVi.Validators
{
    public class IDonViDtoValidator : AbstractValidator<IDonViDto>
    {
        private readonly IDonViRepository _donViRepository;

        public IDonViDtoValidator(IDonViRepository donViRepository)
        {
            _donViRepository = donViRepository;

            RuleFor(p => p.MaLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0."); ;

            RuleFor(p => p.MaLinhVuc).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0."); ;

            RuleFor(p => p.TenDonVi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.DiaChi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.MaSoThue)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.WebSite)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SoDienThoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
