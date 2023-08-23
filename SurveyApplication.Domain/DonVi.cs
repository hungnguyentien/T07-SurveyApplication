using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class DonVi : BaseDomainEntity
    {
        [Required(ErrorMessage = "Mã đơn vị không được để trống")]
        public Guid MaDonVi { get;set; }

        [Required(ErrorMessage = "Mã loại hình không được để trống")]
        public int? MaLoaiHinh { get;set; }

        [Required(ErrorMessage = "Mã lĩnh vực không được để trống")]
        public int? MaLinhVuc { get;set; }

        [Required(ErrorMessage = "Tên đơn vị không được để trống")]
        public string? TenDonVi { get;set; }

        [Required(ErrorMessage = "Đơn vị không được để trống")]
        public string? DiaChi { get;set; }

        [Required(ErrorMessage = "Mã số thuế không được để trống")]
        public string? MaSoThue { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        public string? Email { get;set; }

        [Required(ErrorMessage = "Website không được để trống")]
        public string? WebSite { get;set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string? SoDienThoai { get;set; }
    }
}
