using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DotKhaoSat
{
    public class CreateDotKhaoSatDto : IDotKhaoSatDto
    {
     
        public string MaDotKhaoSat { get; set; }

        public int MaLoaiHinh { get; set; }

        public string TenDotKhaoSat { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuuc { get; set; }
    }
}
