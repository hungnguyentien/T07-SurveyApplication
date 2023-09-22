using MediatR;
using SurveyApplication.Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries
{
    public class GetForgotPasswordRequest : IRequest<ForgotPasswordDto>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
