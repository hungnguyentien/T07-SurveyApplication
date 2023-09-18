using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.DTOs.CauHoi.Validators;

public class CreateCauHoiDtoValidator : AbstractValidator<CreateCauHoiDto>
{
    public CreateCauHoiDtoValidator(ISurveyRepositoryWrapper surveyRepository)
    {
        Include(new CauHoiDtoValidator(surveyRepository));
        RuleFor(x => x.LstCot).Custom((list, context) =>
        {
            var errorName = context.InstanceToValidate.LoaiCauHoi is (int)EnumCauHoi.Type.Radio or (int)EnumCauHoi.Type.CheckBox ? "đá án" : "cột";
            if (list == null) return;
            var checkList = list.GroupBy(x => x.MaCot).Select(x => new { x.Key, Count = x.Count() }).ToList();
            if (checkList.Any(x => x.Count > 1))
                context.AddFailure($"Mã {errorName} {checkList.FirstOrDefault(x => x.Count > 1)?.Key} bị trùng nhau");
        });
        RuleFor(x => x.LstHang).Custom((list, context) =>
        {
            var errorName = context.InstanceToValidate.LoaiCauHoi is (int)EnumCauHoi.Type.Radio or (int)EnumCauHoi.Type.CheckBox ? "đá án" : "hàng";
            if (list == null) return;
            var checkList = list.GroupBy(x => x.MaHang).Select(x => new { x.Key, Count = x.Count() }).ToList();
            if (checkList.Any(x => x.Count > 1))
                context.AddFailure($"Mã {errorName} {checkList.FirstOrDefault(x => x.Count > 1)?.Key} bị trùng nhau");
        });
        RuleForEach(x => x.LstCot).SetValidator(new CotDtoValidator());
        RuleForEach(x => x.LstHang).SetValidator(new HangDtoValidator());
    }
}