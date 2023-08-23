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

        [Required(ErrorMessage = "Mã người đại diện không được để trống")]
        public Guid MaNguoiDaiDien { get; set; }

        [Required(ErrorMessage = "Ma đơn vị không được để trống")]
        public int? MaDonVi { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public  string? HoTen { get; set; }

        [Required(ErrorMessage = "Chức vụ không được để trống")]
        public string? ChucVu { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string? SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string? MoTa { get; set; }
    }
}
