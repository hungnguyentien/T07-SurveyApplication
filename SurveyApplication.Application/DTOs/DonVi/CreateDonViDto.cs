﻿using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DonVi
{
    public class CreateDonViDto :  IDonViDto
    {
        public int Id { get; set; }
        public int? IdLoaiHinh { get; set; }
        public int? IdLinhVuc { get; set; }
        public string? TenDonVi { get; set; }
        public string? DiaChi { get; set; }
        public string? MaSoThue { get; set; }
        public string? Email { get; set; }
        public string? WebSite { get; set; }
        public string? SoDienThoai { get; set; }
    }
}
