using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Persistence.Repositories;

namespace SurveyApplication.Application.DTOs.QuanHuyen.Validators
{
    public class IQuanHuyenDtoValidator : AbstractValidator<IQuanHuyenDto>
    {
        private readonly IQuanHuyenRepository _QuanHuyenRepository;

        public IQuanHuyenDtoValidator(IQuanHuyenRepository QuanHuyenRepository)
        {
            _QuanHuyenRepository = QuanHuyenRepository;

            RuleFor(p => new { p.Name, p.Code })
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(async (model, token) =>
                {
                    var nameExists = await _QuanHuyenRepository.Exists(x => x.Name == model.Name && x.Code != model.Code && !x.Deleted);
                    return !nameExists;
                }).WithMessage("Tên quận huyện đã tồn tại!");

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ParentCode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
