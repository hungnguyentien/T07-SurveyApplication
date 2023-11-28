using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Persistence.Repositories;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;

public class ILoaiHinhDonViDtoValidator : AbstractValidator<ILoaiHinhDonViDto>
{
    private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;

    public ILoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository LoaiHinhDonViRepository)
    {
        _LoaiHinhDonViRepository = LoaiHinhDonViRepository;

        RuleFor(p => new { p.TenLoaiHinh, p.MaLoaiHinh })
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (model, token) =>
            {
                    var nameExists = await _LoaiHinhDonViRepository.Exists(x => x.TenLoaiHinh == model.TenLoaiHinh && x.MaLoaiHinh != model.MaLoaiHinh && !x.Deleted);
                    return !nameExists;
                }).WithMessage("Tên loại hình đã tồn tại!");
    }
}