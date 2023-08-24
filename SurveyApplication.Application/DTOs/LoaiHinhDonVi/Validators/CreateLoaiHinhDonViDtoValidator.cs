using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class CreateLoaiHinhDonViDtoValidator : AbstractValidator<CreateLoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;

        public CreateLoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository loaiHinhDonViRepository)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            Include(new LoaiHinhDonViDtoValidator(_loaiHinhDonViRepository));
        }
    }
}
