using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DotKhaoSat
{
    public partial class DotKhaoSatDto : BaseDto
    {
        public string MaDotKhaoSat { get; set; }
        public int IdLoaiHinh { get; set; }
        public string? TenDotKhaoSat { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? TrangThai { get; set; }

        public string? TenLoaiHinh { get; set; }
        public string? MoTa { get; set; }
    }
}
