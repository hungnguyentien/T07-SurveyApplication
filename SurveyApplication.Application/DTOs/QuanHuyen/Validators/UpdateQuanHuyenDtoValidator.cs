using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.QuanHuyen.Validators;

public class UpdateQuanHuyenDtoValidator : AbstractValidator<UpdateQuanHuyenDto>
{
    private readonly IQuanHuyenRepository _QuanHuyenRepository;

    public UpdateQuanHuyenDtoValidator(IQuanHuyenRepository QuanHuyenRepository)
    {
        _QuanHuyenRepository = QuanHuyenRepository;
        Include(new IQuanHuyenDtoValidator(_QuanHuyenRepository));

        //RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}