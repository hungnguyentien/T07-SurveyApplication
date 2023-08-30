using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class BangKhaoSat : BaseDomainEntity
    {
        [Required]
        public string MaBangKhaoSat { get; set; }

        [Required]
        public int? MaLoaiHinh { get; set; }

        [Required]
        public int? MaDotKhaoSat { get; set; }

        [Required]
        public string? TenBangKhaoSat { get;set; }

        [Required]
        public string? MoTa { get; set; }

        [Required]
        public DateTime? NgayBatDau { get; set; }

        [Required]
        public DateTime? NgayKetThuc { get; set; }

        [Required]
        public int? TrangThai { get; set; }
    }
}
