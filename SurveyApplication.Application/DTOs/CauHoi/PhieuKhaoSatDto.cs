using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.CauHoi
{
    public class PhieuKhaoSatDto: CauHoiDto
    {
        public int IdBangKhaoSat { get; set; }
        public List<CauHoiDto> LstCauHoi { get; set; }
    }
}
