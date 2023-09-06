using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.BangKhaoSat
{
    public partial class BangKhaoSatDto : BaseDto
    {
        public string MaBangKhaoSat { get; set; }
        public int IdLoaiHinh { get; set; }
        public int IdDotKhaoSat { get; set; }
        public string? TenBangKhaoSat { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? TrangThai { get; set; }

        public string? TenDotKhaoSat { get; set; }
        public string? TenLoaiHinh { get; set; }
        /// <summary>
        /// Bảng n-n lưu thông tin câu hỏi và bảng khảo sát
        /// </summary>
        [NotMapped]
        public List<BangKhaoSatCauHoiDto>? BangKhaoSatCauHoi { get; set; }

    }
}
