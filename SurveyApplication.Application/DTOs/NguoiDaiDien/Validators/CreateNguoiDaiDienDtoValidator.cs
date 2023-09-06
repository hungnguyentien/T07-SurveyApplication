using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.NguoiDaiDien.Validators
{
    public class CreateNguoiDaiDienDtoValidator : AbstractValidator<CreateNguoiDaiDienDto>
    {
        private readonly INguoiDaiDienRepository _NguoiDaiDienRepository;

        public CreateNguoiDaiDienDtoValidator(INguoiDaiDienRepository NguoiDaiDienRepository)
        {
            _NguoiDaiDienRepository = NguoiDaiDienRepository;
            Include(new NguoiDaiDienDtoValidator(_NguoiDaiDienRepository));
        }
    }
}
