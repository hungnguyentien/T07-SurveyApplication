using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.XaPhuong.Validators
{
    public class CreateXaPhuongDtoValidator : AbstractValidator<CreateXaPhuongDto>
    {
        private readonly IXaPhuongRepository _XaPhuongRepository;

        public CreateXaPhuongDtoValidator(IXaPhuongRepository XaPhuongRepository)
        {
            _XaPhuongRepository = XaPhuongRepository;
            Include(new XaPhuongDtoValidator(_XaPhuongRepository));
        }
    }
}
