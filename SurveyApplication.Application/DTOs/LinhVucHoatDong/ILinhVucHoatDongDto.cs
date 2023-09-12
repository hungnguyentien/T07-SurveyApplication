﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LinhVucHoatDong
{
    public interface ILinhVucHoatDongDto
    {
        public string MaLinhVuc { get; set; }
        public string? TenLinhVuc { get; set; }
        public string? MoTa { get; set; }
    }
}
