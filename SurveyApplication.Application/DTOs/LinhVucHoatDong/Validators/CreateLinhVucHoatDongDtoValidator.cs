using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.LinhVucHoatDong.Validators
{
    public class CreateLinhVucHoatDongDtoValidator : AbstractValidator<CreateLinhVucHoatDongDto>
    {
        private readonly ILinhVucHoatDongRepository _LinhVucHoatDongRepository;

        public CreateLinhVucHoatDongDtoValidator(ILinhVucHoatDongRepository LinhVucHoatDongRepository)
        {
            _LinhVucHoatDongRepository = LinhVucHoatDongRepository;
            Include(new LinhVucHoatDongDtoValidator(_LinhVucHoatDongRepository));
        }
    }
}
