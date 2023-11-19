using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.Account.Validators;

public class CreateAccountDtoValidator : AbstractValidator<RegisterDto>
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountDtoValidator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
        Include(new AccountDtoValidator(_accountRepository));
    }
}