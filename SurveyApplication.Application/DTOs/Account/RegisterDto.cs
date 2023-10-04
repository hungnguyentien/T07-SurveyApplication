using Microsoft.AspNetCore.Http;
using SurveyApplication.Application.DTOs.Role;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.DTOs.Account
{
    public class RegisterDto
    {
        public string? Address { get; set; }

        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordConfirmed { get; set; }

        public string? Image { get; set; }

        public IFormFile? Img { get; set; }

        public List<MatrixPermission>? MatrixPermission { get; set; }

        public List<string>? LstRoleName { get; set; }
    }
}
