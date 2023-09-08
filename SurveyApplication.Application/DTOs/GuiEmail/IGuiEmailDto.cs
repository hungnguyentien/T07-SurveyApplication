using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.GuiEmail
{
    public interface IGuiEmailDto
    {
        public List<int> LstBangKhaoSat { get; set; }
        public List<int> LstIdDonVi { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
    }
}
