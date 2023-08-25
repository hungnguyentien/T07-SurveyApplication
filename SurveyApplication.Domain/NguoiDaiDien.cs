using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class NguoiDaiDien : BaseDomainEntity
    {
        public Guid MaNguoiDaiDien { get; set; }

        [Required]
        public int? MaDonVi { get; set; }

        [Required]
        public  string? HoTen { get; set; }

        [Required]
        public string? ChucVu { get; set; }

        [Required]
        public string? SoDienThoai { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? MoTa { get; set; }
    }
}
