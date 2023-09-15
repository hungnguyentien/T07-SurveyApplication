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
        [MaxLength(250)]
        public string MaDonVi { get;set; }

        public int IdLoaiHinh { get;set; }

        public int IdLinhVuc { get;set; }

        public int IdTinhTp { get; set; }

        public int IdQuanHuyen { get; set; }

        public int IdXaPhuong { get; set; }

        [Required]
        public string TenDonVi { get;set; }

        [Required]
        public string DiaChi { get;set; }

        [Required]
        public string MaSoThue { get; set; }

        [Required]
        public string Email { get;set; }

        [Required]
        public string WebSite { get;set; }

        [Required]
        public string SoDienThoai { get;set; }
    }
}
