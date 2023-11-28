using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.CauHoi.Validators;

public class CauHoiDtoValidator : AbstractValidator<ICauHoiDto>
{
    public CauHoiDtoValidator(ISurveyRepositoryWrapper surveyRepository, int id = 0)
    {
        RuleFor(p => p.MaCauHoi)
            .NotNull().NotEmpty().WithMessage("Mã câu hỏi không được để trống!");
        RuleFor(p => p.MaCauHoi)
            .MustAsync(async (maCauHoi, _) => !await surveyRepository.CauHoi.Exists(x =>
                x.MaCauHoi == maCauHoi && !x.Deleted && (id == 0 || (id > 0 && x.Id != id))))
            .WithMessage("Mã câu hỏi đã rồn tại!");
    }
}