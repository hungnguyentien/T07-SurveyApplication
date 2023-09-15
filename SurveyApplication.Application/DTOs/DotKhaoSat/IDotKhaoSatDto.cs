namespace SurveyApplication.Application.DTOs.DotKhaoSat;

public interface IDotKhaoSatDto
{
    public string MaDotKhaoSat { get; set; }

    public int IdLoaiHinh { get; set; }

    public string TenDotKhaoSat { get; set; }

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }
    public int? TrangThai { get; set; }
}