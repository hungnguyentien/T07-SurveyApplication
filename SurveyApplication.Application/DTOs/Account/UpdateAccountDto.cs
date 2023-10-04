using Microsoft.AspNetCore.Http;
using SurveyApplication.Application.DTOs.Role;

namespace SurveyApplication.Application.DTOs.Account
{
    public class UpdateAccountDto : AccountDto
    {
        public string? Image { get; set; }

        public IFormFile? Img { get; set; }
        public List<MatrixPermission>? MatrixPermission { get; set; }
        public List<string>? LstRoleName { get; set; }
    }
}

