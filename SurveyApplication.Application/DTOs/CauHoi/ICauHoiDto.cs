namespace SurveyApplication.Application.DTOs.CauHoi;

public interface ICauHoiDto
{
    public int Id { get; set; }
    public string MaCauHoi { get; set; }
    public short LoaiCauHoi { get; set; }
    public bool? BatBuoc { get; set; }
    public string TieuDe { get; set; }
    public string? NoiDung { get; set; }
    public int? SoLuongFileToiDa { get; set; }
    public int? KichThuocFile { get; set; }
    public bool? IsOther { get; set; }
    public string? LabelCauTraLoi { get; set; }
}