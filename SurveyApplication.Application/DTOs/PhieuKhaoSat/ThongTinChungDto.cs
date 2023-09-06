using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;

namespace SurveyApplication.Application.DTOs.PhieuKhaoSat
{
    public class ThongTinChungDto
    {
        public DonViDto DonVi { get; set; }
        public NguoiDaiDienDto NguoiDaiDien { get; set; }
        /// <summary>
        /// Id bảng khảo sát
        /// </summary>
        public int BangKhaoSat { get; set; }
        /// <summary>
        /// Tráng thái bảng khảo sát
        /// </summary>
        public int TrangThai { get; set; }
    }
}
