using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi;

public class UpdateLoaiHinhDonViDto : BaseDto, ILoaiHinhDonViDto
{
    public string MaLoaiHinh { get; set; } = null!;
    public string? TenLoaiHinh { get; set; }
    public string? MoTa { get; set; }
}