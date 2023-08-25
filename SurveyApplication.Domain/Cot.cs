using SurveyApplication.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Domain
{
    public class Cot : BaseDomainEntity
    {
        [Required]
        [MaxLength(250)]
        public string MaCot { get; set; }
         [Required]
        [MaxLength(250)]
        public string NoiDung { get; set; }
        public int IdCauHoi { get; set; }
    }
}
