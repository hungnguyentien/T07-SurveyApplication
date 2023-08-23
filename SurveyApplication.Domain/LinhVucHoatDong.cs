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
        [Required(ErrorMessage = "Mã lĩnh vực không được để trống")]
        public Guid MaLinhVuc { get; set; }

        [Required(ErrorMessage = "Tên lĩnh vực không được để trống")]
        public string? TenLinhVuc { get; set; }  
    }
}
