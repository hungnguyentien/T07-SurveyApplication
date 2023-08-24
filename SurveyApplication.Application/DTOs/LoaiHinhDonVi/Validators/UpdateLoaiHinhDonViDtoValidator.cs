using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class UpdateLoaiHinhDonViDtoValidator : AbstractValidator<UpdateLoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;

        public UpdateLoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository loaiHinhDonViRepository)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            Include(new ILoaiHinhDonViDtoValidator(_loaiHinhDonViRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
