using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien
{
    public class UpdateDonViAndNguoiDaiDienDto
    {
        public UpdateDonViDto? DonViDto { get; set; }
        public UpdateNguoiDaiDienDto? NguoiDaiDienDto { get; set; }
    }
}
