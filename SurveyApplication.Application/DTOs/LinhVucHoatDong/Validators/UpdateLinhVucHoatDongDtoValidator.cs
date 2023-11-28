using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.LinhVucHoatDong.Validators
{
    public class UpdateLinhVucHoatDongDtoValidator : AbstractValidator<UpdateLinhVucHoatDongDto>
    {
        private readonly ILinhVucHoatDongRepository _LinhVucHoatDongRepository;

        public UpdateLinhVucHoatDongDtoValidator(ILinhVucHoatDongRepository LinhVucHoatDongRepository)
        {
            _LinhVucHoatDongRepository = LinhVucHoatDongRepository;
            Include(new ILinhVucHoatDongDtoValidator(_LinhVucHoatDongRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
