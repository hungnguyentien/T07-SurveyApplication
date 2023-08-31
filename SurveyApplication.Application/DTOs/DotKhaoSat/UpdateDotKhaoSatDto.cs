using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DotKhaoSat
{
    public class UpdateDotKhaoSatDto :BaseDto , IDotKhaoSatDto
    {
        public string MaDotKhaoSat { get; set; }

        public int MaLoaiHinh { get; set; }

        public string TenDotKhaoSat { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuuc { get; set; }
        public int? TrangThai { get; set; }

    }
}
