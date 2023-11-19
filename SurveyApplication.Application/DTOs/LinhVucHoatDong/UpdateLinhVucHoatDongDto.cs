using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.LinhVucHoatDong;

public class UpdateLinhVucHoatDongDto : BaseDto, ILinhVucHoatDongDto
{
    public string MaLinhVuc { get; set; }
    public string? TenLinhVuc { get; set; }
    public string? MoTa { get; set; }
}