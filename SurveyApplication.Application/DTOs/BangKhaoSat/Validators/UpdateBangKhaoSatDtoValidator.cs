using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.BangKhaoSat.Validators
{
    public class UpdateBangKhaoSatDtoValidator : AbstractValidator<UpdateBangKhaoSatDto>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;

        public UpdateBangKhaoSatDtoValidator(IBangKhaoSatRepository bangKhaoSatRepository)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            Include(new IBangKhaoSatDtoValidator(_bangKhaoSatRepository));
            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
