using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;

namespace SurveyApplication.Application.DTOs.PhieuKhaoSat;

public class ThongTinChungDto
{
    public DonViDto DonVi { get; set; }
    public NguoiDaiDienDto NguoiDaiDien { get; set; }

    /// <summary>
    ///     Id gửi email
    /// </summary>
    public int IdGuiEmail { get; set; }

    /// <summary>
    ///     Tráng thái kết quả
    /// </summary>
    public int TrangThaiKq { get; set; }
}