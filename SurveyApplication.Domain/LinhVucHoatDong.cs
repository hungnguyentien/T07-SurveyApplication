using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class LinhVucHoatDong : BaseDomainEntity
    {
        [Required]
        public Guid MaLinhVuc { get; set; }

        [Required]
        public string? TenLinhVuc { get; set; }  
    }
}
