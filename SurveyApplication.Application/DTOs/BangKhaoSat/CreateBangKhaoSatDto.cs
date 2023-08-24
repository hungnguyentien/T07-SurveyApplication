using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.BangKhaoSat
{
    public class CreateBangKhaoSatDto :  IBangKhaoSatDto
    {
        public string MaBangKhaoSat { get; set; }
        public string MaLoaiHinh { get; set; }
        public string MaDotKhaoSat { get; set; }
        public string TenBangKhaoSat { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}
