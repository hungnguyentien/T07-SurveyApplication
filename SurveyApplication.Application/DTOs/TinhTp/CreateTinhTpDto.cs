﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.TinhTp
{
    public  class CreateTinhTpDto :ITinhTpDto
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Type { get; set; }
    }
}
