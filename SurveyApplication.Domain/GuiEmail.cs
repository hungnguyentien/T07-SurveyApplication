﻿using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class GuiEmail : BaseDomainEntity
    {
        public Guid MaGuiEmail { get; set; }
        public string? DiaChiNhan { get; set; }
        public string? TieuDe { get; set; }
        public string? NoiDung { get; set; }
      
    }
}
