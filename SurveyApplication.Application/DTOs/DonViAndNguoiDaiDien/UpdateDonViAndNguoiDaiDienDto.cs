using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;

namespace SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien
{
    public class UpdateDonViAndNguoiDaiDienDto
    {
        public UpdateDonViDto? DonViDto { get; set; }
        public UpdateNguoiDaiDienDto? NguoiDaiDienDto { get; set; }
    }
}
