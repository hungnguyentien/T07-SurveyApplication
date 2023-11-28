using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.Account.Validators
{
    public class UpdateAccountDtoValidator : AbstractValidator<UpdateAccountDto>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateAccountDtoValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            Include(new IAccountDtoValidator(_accountRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
