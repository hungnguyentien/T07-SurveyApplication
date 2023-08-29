using MediatR;
using SurveyApplication.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Auths.Requests.Queries
{
    public class LoginRequest : IRequest<AuthDto>
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
