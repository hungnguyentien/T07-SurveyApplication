using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DonVi
{
    public interface IDonViDto
    {
        public Guid? MaDonVi { get; set; }
        public int? MaLoaiHinh { get; set; }
        public int? MaLinhVuc { get; set; }
        public string? TenDonVi { get; set; }
        public string? DiaChi { get; set; }
        public string? MaSoThue { get; set; }
        public string? Email { get; set; }
        public string? WebSite { get; set; }
        public string? SoDienThoai { get; set; }
    }
}
