using FluentValidation;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.Account.Validators
{
    public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountDtoValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            Include(new AccountDtoValidator(_accountRepository));
        }
    }
}
