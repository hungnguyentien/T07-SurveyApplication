using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.PhieuKhaoSat
{
    public interface IKetQuaDto
    {
        public int IdDonVi { get; set; }
        public int IdNguoiDaiDien { get; set; }
        public int IdBangKhaoSat { get; set; }
        public string Data { get; set; }
    }
}
