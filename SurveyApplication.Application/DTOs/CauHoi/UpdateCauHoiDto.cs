using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApplication.Application.DTOs.CauHoi;

public class UpdateCauHoiDto : ICauHoiDto
{
    [NotMapped] public List<CotDto>? LstCot { get; set; }

    [NotMapped] public List<HangDto>? LstHang { get; set; }

    public int Id { get; set; }
    public string MaCauHoi { get; set; }
    public short LoaiCauHoi { get; set; }
    public bool? BatBuoc { get; set; }
    public string TieuDe { get; set; }
    public string NoiDung { get; set; }
    public int? SoLuongFileToiDa { get; set; }
    public int? KichThuocFile { get; set; }
    public bool? IsOther { get; set; }
    public string? LabelCauTraLoi { get; set; }
}