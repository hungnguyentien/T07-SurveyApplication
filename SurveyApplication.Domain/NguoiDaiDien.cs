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
        [MaxLength(250)]
        public string MaNguoiDaiDien { get; set; }

        public int IdDonVi { get; set; }

        [Required]
        public  string HoTen { get; set; }

        [Required]
        public string ChucVu { get; set; }

        [Required]
        public string SoDienThoai { get; set; }

        [Required]
        public string Email { get; set; }
        public string? MoTa { get; set; }
    }
}
