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
        [MaxLength(250)]
        public string MaDotKhaoSat { get; set; }

        public int IdLoaiHinh { get; set; }

        [Required]
        public string TenDotKhaoSat { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        public int? TrangThai { get; set; }
    }
}
