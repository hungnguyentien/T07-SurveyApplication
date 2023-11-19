using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.LinhVucHoatDong;

public class LinhVucHoatDongDto : BaseDto
{
    public string MaLinhVuc { get; set; }
    public string? TenLinhVuc { get; set; }
    public string? MoTa { get; set; }
}