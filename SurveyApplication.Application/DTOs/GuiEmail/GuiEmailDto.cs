using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.GuiEmail
{
    public partial class GuiEmailDto : BaseDto
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

    }
}
