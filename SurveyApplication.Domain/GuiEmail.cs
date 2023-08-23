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
        [Required(ErrorMessage = "Mã gửi email không được để trống")]
        public Guid MaGuiEmail { get; set; }

        [Required(ErrorMessage = "Địa  chỉ nhận không được để trống")]
        public string? DiaChiNhan { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string? TieuDe { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string? NoiDung { get; set; }
      
    }
}
