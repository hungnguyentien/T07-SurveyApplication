using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApplication.Application.DTOs.BangKhaoSat
{
    public class CreateBangKhaoSatDto : IBangKhaoSatDto
    {
        public string MaBangKhaoSat { get; set; }
        public int? IdLoaiHinh { get; set; }
        public int? IdDotKhaoSat { get; set; }
        public string? TenBangKhaoSat { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? TrangThai { get; set; }
        /// <summary>
        /// Bảng n-n lưu thông tin câu hỏi và bảng khảo sát
        /// </summary>
        [NotMapped]
        public List<BangKhaoSatCauHoiDto>? BangKhaoSatCauHoi { get; set; }

    }
}
