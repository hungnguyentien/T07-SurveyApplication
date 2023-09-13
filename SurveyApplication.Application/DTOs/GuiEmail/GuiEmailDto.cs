using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.GuiEmail;

public class GuiEmailDto : BaseDto
{
    public string MaGuiEmail { get; set; }
    public string DiaChiNhan { get; set; }
    public string TieuDe { get; set; }
    public string NoiDung { get; set; }
    public int TrangThai { get; set; }
    public DateTime ThoiGian { get; set; }
    public int IdBangKhaoSat { get; set; }
    public string TenBangKhaoSat { get; set; }
    public int IdDonVi { get; set; }
    public string TenDonVi { get; set; }
    public string NguoiThucHien { get; set; }
}

public class GuiEmailBksDto
{
    public int IdBangKhaoSat { get; set; }
    public string MaBangKhaoSat { get; set; }
    public string TenBangKhaoSat { get; set; }
    public int CountSendEmail { get; set; }
    public int CountSendThanhCong { get; set; }
    public int CountSendLoi { get; set; }
    public int CountSendThuHoi { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }

}

public class GuiEmailBksDetailDto
{
    public string MaBangKhaoSat { get; set; }
    public string TenBangKhaoSat { get; set; }
    public List<GuiEmailDto> LstGuiEmail { get; set; }

}