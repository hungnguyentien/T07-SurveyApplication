using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.CauHoi.Validators
{
    public class UpdateCauHoiDtoValidator : AbstractValidator<UpdateCauHoiDto>
    {
        public UpdateCauHoiDtoValidator(ISurveyRepositoryWrapper surveyRepository, int idCauHoi)
        {
            Include(new CauHoiDtoValidator(surveyRepository, idCauHoi));
            RuleForEach(x => x.LstCot).SetValidator(new CotDtoValidator());
            RuleForEach(x => x.LstHang).SetValidator(new HangDtoValidator());
        }
    }
}
