using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.DonVi;

public class UpdateDonViDto : BaseDto, IDonViDto
{
    public int Id { get; set; }
    public string? MaDonVi { get; set; }
    public int? IdLoaiHinh { get; set; }
    public int? IdLinhVuc { get; set; }
    public int? IdTinhTp { get; set; }
    public int? IdQuanHuyen { get; set; }
    public int? IdXaPhuong { get; set; }
    public string? TenDonVi { get; set; }
    public string? DiaChi { get; set; }
    public string? MaSoThue { get; set; }
    public string? Email { get; set; }
    public string? WebSite { get; set; }
    public string? SoDienThoai { get; set; }
}