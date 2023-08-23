using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class DotKhaoSat : BaseDomainEntity
    {
        [Required]
        public string MaDotKhaoSat { get; set; }

        [Required]
        public int? MaLoaiHinh { get; set; }

        [Required]
        public string? TenDotKhaoSat { get; set; }

        [Required]
        public DateTime? NgayBatDau { get; set; }

        [Required]
        public DateTime? NgayKetThuuc { get; set; }

        
    }
}
