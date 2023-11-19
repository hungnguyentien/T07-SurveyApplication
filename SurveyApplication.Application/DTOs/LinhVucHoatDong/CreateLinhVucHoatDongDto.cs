namespace SurveyApplication.Application.DTOs.LinhVucHoatDong;

public class CreateLinhVucHoatDongDto : ILinhVucHoatDongDto
{
    public string MaLinhVuc { get; set; }
    public string? TenLinhVuc { get; set; }
    public string? MoTa { get; set; }
}