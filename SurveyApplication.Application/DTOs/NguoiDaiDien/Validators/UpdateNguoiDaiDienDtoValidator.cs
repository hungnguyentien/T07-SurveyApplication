using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

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
