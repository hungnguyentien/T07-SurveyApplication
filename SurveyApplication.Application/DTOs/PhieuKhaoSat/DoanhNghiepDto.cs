namespace SurveyApplication.Application.DTOs.PhieuKhaoSat
{
    public class UpdateDoanhNghiepDto
    {
        public DoanhNghiepDto DonVi { get; set; }
        public NguoiDaiDienDnDto NguoiDaiDien { get; set; }
        public string IdGuiEmail { get; set; }
    }

    public class DoanhNghiepDto
    {
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

    public class NguoiDaiDienDnDto
    {
        public string? HoTen { get; set; }
        public string? ChucVu { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
    }
}
