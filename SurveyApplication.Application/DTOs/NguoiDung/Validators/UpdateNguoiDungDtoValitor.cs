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
    
    public class UpdateNguoiDungDtoValitor : AbstractValidator<UpdateNguoiDungDto>
    {
        private readonly INguoiDungRepository _NguoiDungRepository;

        public UpdateNguoiDungDtoValitor(INguoiDungRepository NguoiDungRepository)
        {
            _NguoiDungRepository = NguoiDungRepository;
            Include(new INguoiDungDtoValitor(_NguoiDungRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
