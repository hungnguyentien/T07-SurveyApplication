using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class NguoiDaiDien : BaseDomainEntity
    {
        public Guid MaNguoiDaiDien { get; set; }
        public Guid? MaDonVi { get; set; }
        public  string? HoTen { get; set; }
        public string? ChucVu { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? MoTa { get; set; }
    }
}
