using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
