using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.Account
{
    public partial class AccountDto : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
    }
}
