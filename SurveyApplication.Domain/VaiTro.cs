﻿using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class VaiTro : BaseDomainEntity
    {


        [Required]
        public int MaVaiTro { get; set; }


        [Required]
        public string? TenVaiTro { get; set; }

    }
}
