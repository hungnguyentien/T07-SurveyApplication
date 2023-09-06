﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DotKhaoSat
{
    public interface IDotKhaoSatDto
    {

        public string MaDotKhaoSat { get; set; }

        public int IdLoaiHinh { get; set; }

        public string TenDotKhaoSat { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuuc { get; set; }
        public int? TrangThai { get; set; }

    }
}
