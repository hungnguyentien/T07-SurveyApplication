using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public class Hang : BaseDomainEntity
    {
        [Required]
        [MaxLength(50)]
        public string MaHang { get; set; }
        [Required]
        [MaxLength(500)]
        public string NoiDung { get; set; }
        public bool? IsOther { get; set; }
        public string LabelCauTraLoi { get; set; }
        public int IdCauHoi { get; set; }
    }
}
