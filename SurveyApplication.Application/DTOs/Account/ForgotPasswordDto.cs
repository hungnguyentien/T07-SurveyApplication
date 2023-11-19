using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.DTOs.Account;

public class ForgotPasswordDto
{
    [Required] [EmailAddress] public string Email { get; set; }
}