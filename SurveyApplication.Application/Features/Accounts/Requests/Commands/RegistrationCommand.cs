using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands
{
    public class RegistrationCommand : IRequest<BaseCommandResponse>
    {
        public CreateAccountDto? AccountDto { get; set; }
        public string? RoleName { get; set; }
    }
}
