using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class GuiEmail : BaseDomainEntity
    {
        [Required]
        public Guid MaGuiEmail { get; set; }

        [Required]
        public string? DiaChiNhan { get; set; }

        [Required]
        public string? TieuDe { get; set; }

        [Required]
        public string? NoiDung { get; set; }
      
    }
}
