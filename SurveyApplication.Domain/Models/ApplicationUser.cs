using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Utility.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApplication.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? Img { get; set; }
        public int? ActiveFlag { get; set; } = (int)EnumCommon.ActiveFlag.Active;
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
