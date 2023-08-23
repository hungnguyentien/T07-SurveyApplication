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
        [Required]
        public Guid MaDonVi { get;set; }

        [Required]
        public int? MaLoaiHinh { get;set; }

        [Required]
        public int? MaLinhVuc { get;set; }

        [Required]
        public string? TenDonVi { get;set; }

        [Required]
        public string? DiaChi { get;set; }

        [Required]
        public string? MaSoThue { get; set; }

        [Required]
        public string? Email { get;set; }

        [Required]
        public string? WebSite { get;set; }

        [Required]
        public string? SoDienThoai { get;set; }
    }
}
