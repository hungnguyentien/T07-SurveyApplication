namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi;

public class CreateLoaiHinhDonViDto : ILoaiHinhDonViDto
{
    public string MaLoaiHinh { get; set; } = null!;
    public string? TenLoaiHinh { get; set; }
    public string? MoTa { get; set; }
}