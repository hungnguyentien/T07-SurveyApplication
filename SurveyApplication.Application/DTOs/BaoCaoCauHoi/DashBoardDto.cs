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
        public double CountDonViBo { get; set; }
        public double CountDonViSo { get; set; }
        public double CountDonViNganh { get; set; }
        public double CountDot1 { get; set; }
        public double CountDot2 { get; set; }
        public double CountDot3 { get; set; }

        public List<ListTinhTp>? ListTinhTp { get; set; }
    }
}
