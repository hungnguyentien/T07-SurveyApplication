using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;

namespace SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien;

public class CreateDonViAndNguoiDaiDienDto
{
    public CreateDonViDto? DonViDto { get; set; }
    public CreateNguoiDaiDienDto? NguoiDaiDienDto { get; set; }
}