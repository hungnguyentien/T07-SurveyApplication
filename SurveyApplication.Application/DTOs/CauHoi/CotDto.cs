﻿using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.CauHoi
{
    public class CotDto
    {
        /// <summary>
        /// value
        /// </summary>
        public string MaCot { get; set; }
        /// <summary>
        /// lable (text)
        /// </summary>
        public string Noidung { get; set; }

        public int Id { get; set; }
    }
}
