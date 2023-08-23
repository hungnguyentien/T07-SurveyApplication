﻿using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public class CauHoiPhu : BaseDomainEntity
    {
        [Required]
        public string MaCauHoi { get; set; }
        public string NoiDung { get; set; }
        public int IdCauHoi { get; set; }
    }
}
