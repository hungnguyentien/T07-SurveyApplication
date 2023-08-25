using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class CreateLoaiHinhDonViDtoValidator : AbstractValidator<CreateLoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;

        public CreateLoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository LoaiHinhDonViRepository)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            Include(new LoaiHinhDonViDtoValidator(_LoaiHinhDonViRepository));
        }
    }
}
