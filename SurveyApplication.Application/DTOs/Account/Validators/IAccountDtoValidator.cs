using FluentValidation;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.Account.Validators
{
    public class IAccountDtoValidator : AbstractValidator<AccountDto>
    {
        private readonly IAccountRepository _accountRepository;

        public IAccountDtoValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
