using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.Account.Validators
{
    public class AccountDtoValidator : AbstractValidator<RegisterDto>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountDtoValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(p => p.Email)
                .MustAsync(async (email, token) =>
                {
                    var bangKhaoSatViExists = await _accountRepository.Exists(x => x.Email == email);
                    return !bangKhaoSatViExists;
                }).WithMessage("Email người dùng đã tồn tại!");

            RuleFor(user => user.UserName).NotEmpty().WithMessage("Tên người dùng không được trống.");

            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("Địa chỉ email không hợp lệ.");
        }
    }
}
