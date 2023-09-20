using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.DTOs.Account
{
    public class AccountDto : IdentityUser
    {
        public string? Address { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
