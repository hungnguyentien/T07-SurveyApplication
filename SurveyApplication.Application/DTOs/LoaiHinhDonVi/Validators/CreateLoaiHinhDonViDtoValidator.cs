using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class CreateLoaiHinhDonViDtoValidator : AbstractValidator<CreateLoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;

        public CreateLoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository LoaiHinhDonViRepository)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            Include(new LoaiHinhDonViDtoValidator(_LoaiHinhDonViRepository));
        }
    }
}
