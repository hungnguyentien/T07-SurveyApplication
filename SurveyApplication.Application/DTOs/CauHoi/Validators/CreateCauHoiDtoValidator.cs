using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.CauHoi.Validators
{
    public class CreateCauHoiDtoValidator : AbstractValidator<CreateCauHoiDto>
    {
        public CreateCauHoiDtoValidator(ISurveyRepositoryWrapper surveyRepository)
        {
            Include(new CauHoiDtoValidator(surveyRepository));
            RuleForEach(x => x.LstCot).SetValidator(new CotDtoValidator());
            RuleForEach(x => x.LstHang).SetValidator(new HangDtoValidator());
        }
    }
}
