using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.DonVi;

public class DonViDto : BaseDto
{
    public string? MaDonVi { get; set; }
    public int? IdLoaiHinh { get; set; }
    public int? IdLinhVuc { get; set; }
    public string? TenDonVi { get; set; }
    public string? DiaChi { get; set; }
    public string? MaSoThue { get; set; }
    public string? Email { get; set; }
    public string? WebSite { get; set; }
    public string? SoDienThoai { get; set; }

    public int? IdDonVi { get; set; }
    public int? IdNguoiDaiDien { get; set; }

    public string? EmailDonVi { get; set; }
    public string? SoDienThoaiDonVi { get; set; }

    public string? HoTen { get; set; }
    public string? TenLoaiHinh { get; set; }
    public string? ChucVu { get; set; }
    public string? MoTa { get; set; }
    public string? EmailNguoiDaiDien { get; set; }
    public string? SoDienThoaiNguoiDaiDien { get; set; }
}