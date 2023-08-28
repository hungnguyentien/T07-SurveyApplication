using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class NguoiDung : BaseDomainEntity
    {
        [Required]
        public int MaNguoiDung { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? PassWord { get;set; }

        [Required]
        public string? HoTen { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? DiaChi { get; set; }

        [Required]
        public string? SoDienThoai { get; set; }

        [Required]
        public DateTime? NgaySinh { get; set; }
    }
}
