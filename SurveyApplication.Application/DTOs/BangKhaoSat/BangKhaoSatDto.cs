using System.ComponentModel.DataAnnotations.Schema;
using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.BangKhaoSat;

public class BangKhaoSatDto : BaseDto
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
    public int? TrangThaiEmail { get; set; }

    public bool? IsRequired { get; set; }
    public string PanelTitle { get; set; }
    public int IdBangKhaoSat { get; set; }

    /// <summary>
    ///     Bảng n-n lưu thông tin câu hỏi và bảng khảo sát
    /// </summary>
    [NotMapped]
    public List<BangKhaoSatCauHoiDto>? BangKhaoSatCauHoi { get; set; }
}