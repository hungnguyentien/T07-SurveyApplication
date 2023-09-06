using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class UpdateLoaiHinhDonViDtoValidator : AbstractValidator<UpdateLoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;

        public UpdateLoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository LoaiHinhDonViRepository)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            Include(new ILoaiHinhDonViDtoValidator(_LoaiHinhDonViRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
