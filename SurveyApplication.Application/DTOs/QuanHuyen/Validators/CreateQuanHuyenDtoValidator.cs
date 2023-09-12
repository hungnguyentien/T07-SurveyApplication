using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.QuanHuyen.Validators
{
    public class CreateQuanHuyenDtoValidator : AbstractValidator<CreateQuanHuyenDto>
    {
        private readonly IQuanHuyenRepository _QuanHuyenRepository;

        public CreateQuanHuyenDtoValidator(IQuanHuyenRepository QuanHuyenRepository)
        {
            _QuanHuyenRepository = QuanHuyenRepository;
            Include(new QuanHuyenDtoValidator(_QuanHuyenRepository));
        }
    }
}
