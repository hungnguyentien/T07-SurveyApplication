using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class ILoaiHinhDonViDtoValidator : AbstractValidator<ILoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;

        public ILoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository loaiHinhDonViRepository)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;

            RuleFor(p => p.TenLoaiHinh)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.MoTa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
