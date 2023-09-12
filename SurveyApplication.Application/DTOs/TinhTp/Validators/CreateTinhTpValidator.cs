using FluentValidation;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApplication.Persistence.Repositories;

namespace SurveyApplication.Application.DTOs.TinhTp.Validators
{
    public class CreateTinhTpValidator : AbstractValidator<CreateTinhTpDto>
    {
        private readonly ITinhTpRepository _tinhTpRepository;

        public CreateTinhTpValidator(ITinhTpRepository tinhTpRepository)
        {
            _tinhTpRepository = tinhTpRepository;
            Include(new TinhTpDtoValidator(_tinhTpRepository));
          
        }
    }
}
