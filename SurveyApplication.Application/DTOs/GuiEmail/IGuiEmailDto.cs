using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.GuiEmail
{
    public interface IGuiEmailDto
    {
        public Guid MaGuiEmail { get; set; }
        public int MaBangKhaoSat { get; set; }
        public int? TrangThai { get; set; }
        public DateTime? ThoiGian { get; set; }
        public string DiaChiNhan { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
    }
}
