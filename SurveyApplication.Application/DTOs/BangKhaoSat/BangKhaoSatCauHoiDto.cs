using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.BangKhaoSat;

public class BangKhaoSatCauHoiDto : BaseDto
{
    public int? IdBangKhaoSat { get; set; }
    public int IdCauHoi { get; set; }
    public int? Priority { get; set; }
    public bool? IsRequired { get; set; } = false;

    public string? MaCauHoi { get; set; }
    public string? TieuDe { get; set; }
}