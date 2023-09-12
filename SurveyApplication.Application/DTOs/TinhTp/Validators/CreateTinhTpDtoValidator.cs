using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.TinhTp.Validators
{
    public class CreateTinhTpDtoValidator : AbstractValidator<CreateTinhTpDto>
    {
        private readonly ITinhTpRepository _TinhTpRepository;

        public CreateTinhTpDtoValidator(ITinhTpRepository TinhTpRepository)
        {
            _TinhTpRepository = TinhTpRepository;
            Include(new TinhTpDtoValidator(_TinhTpRepository));
        }
    }
}
