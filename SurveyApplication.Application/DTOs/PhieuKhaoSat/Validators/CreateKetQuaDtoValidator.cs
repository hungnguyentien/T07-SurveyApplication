using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.PhieuKhaoSat.Validators
{
    public class CreateKetQuaDtoValidator : AbstractValidator<CreateKetQuaDto>
    {
        private readonly IKetQuaRepository _ketQuaRepository;

        public CreateKetQuaDtoValidator(IKetQuaRepository ketQuaRepository)
        {
            _ketQuaRepository = ketQuaRepository;
            Include(new KetQuaDtoValidator(_ketQuaRepository));
        }
    }
}
