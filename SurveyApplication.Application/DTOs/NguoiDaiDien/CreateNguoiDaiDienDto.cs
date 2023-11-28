namespace SurveyApplication.Application.DTOs.NguoiDaiDien;

public class CreateNguoiDaiDienDto : INguoiDaiDienDto
{
    public int Id { get; set; }
    public int? IdDonVi { get; set; }
    public string? HoTen { get; set; }
    public string? ChucVu { get; set; }
    public string? SoDienThoai { get; set; }
    public string? Email { get; set; }
    public string? MoTa { get; set; }
}