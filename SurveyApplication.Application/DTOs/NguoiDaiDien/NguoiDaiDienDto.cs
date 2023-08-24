using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDaiDien
{
    public partial class NguoiDaiDienDto : BaseDto
    {
        public Guid MaNguoiDaiDien { get; set; }
        public int? MaDonVi { get; set; }
        public string? HoTen { get; set; }
        public string? ChucVu { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? MoTa { get; set; }
    }
}
