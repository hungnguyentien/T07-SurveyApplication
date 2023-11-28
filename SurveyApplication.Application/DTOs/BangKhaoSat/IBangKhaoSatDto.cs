namespace SurveyApplication.Application.DTOs.BangKhaoSat;

public interface IBangKhaoSatDto
{
    public string MaBangKhaoSat { get; set; }
    public int? IdLoaiHinh { get; set; }
    public int? IdDotKhaoSat { get; set; }
    public string? TenBangKhaoSat { get; set; }
    public string? MoTa { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public int? TrangThai { get; set; }
}