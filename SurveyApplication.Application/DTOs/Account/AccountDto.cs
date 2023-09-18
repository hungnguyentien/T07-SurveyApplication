using Microsoft.AspNetCore.Identity;

namespace SurveyApplication.Application.DTOs.Account
{
    public class AccountDto : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
