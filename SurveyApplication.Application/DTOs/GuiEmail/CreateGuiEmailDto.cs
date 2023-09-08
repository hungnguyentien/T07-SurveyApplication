using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.GuiEmail
{
    public class CreateGuiEmailDto : IGuiEmailDto
    {
        [NotMapped]
        public List<int> LstBangKhaoSat { get; set; }
        [NotMapped]
        public List<int> LstIdDonVi { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
    }
}
