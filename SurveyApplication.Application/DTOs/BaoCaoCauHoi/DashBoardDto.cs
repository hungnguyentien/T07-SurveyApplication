using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi
{
    public class DashBoardDto
    {
        public int CountDotKhaoSat { get; set; }
        public int CountBangKhaoSat { get; set; }
        public int CountThamGia { get; set; }
        public List<ListTinhTp>? ListTinhTp { get; set; }
        public List<CountDonViByLoaiHinh> LstCountDonViByLoaiHinh { get; set; }
        public List<CountDot> LstCountDot { get; set; }
    }

    public class CountDonViByLoaiHinh
    {
        public string Ten { get; set; }
        public double Count { get; set; }
    }

    public class CountDot
    {
        public string Ten { get; set; }
        public double Count { get; set; }
    }
}
