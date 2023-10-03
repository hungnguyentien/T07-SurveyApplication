using Microsoft.AspNetCore.Http;

namespace SurveyApplication.Application.DTOs.Account
{
    public class UpdateAccountDto : AccountDto
    {
        public string? Image { get; set; }

        public IFormFile? Img { get; set; }
    }
}
