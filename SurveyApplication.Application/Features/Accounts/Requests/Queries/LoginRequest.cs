using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries
{
    public class LoginRequest : IdentityUser, IRequest<AuthResponse>
    {
        public string Password { get; set; }
    }
}
