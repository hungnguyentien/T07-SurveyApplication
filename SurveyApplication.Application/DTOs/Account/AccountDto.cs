using Microsoft.AspNetCore.Identity;

namespace SurveyApplication.Application.DTOs.Account;

public class AccountDto : IdentityUser
{
    public string Password { get; set; }
}