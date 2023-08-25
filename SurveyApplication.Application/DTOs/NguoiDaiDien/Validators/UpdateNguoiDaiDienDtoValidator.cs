using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDaiDien.Validators
{
    public class UpdateNguoiDaiDienDtoValidator : AbstractValidator<UpdateNguoiDaiDienDto>
    {
        private readonly INguoiDaiDienRepository _NguoiDaiDienRepository;

        public UpdateNguoiDaiDienDtoValidator(INguoiDaiDienRepository NguoiDaiDienRepository)
        {
            _NguoiDaiDienRepository = NguoiDaiDienRepository;
            Include(new INguoiDaiDienDtoValidator(_NguoiDaiDienRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
