﻿using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public class Module : BaseDomainEntity
    {
        [Required] [MaxLength(250)] public string Name { get; set; }
        [Required] public string RouterLink { get; set; }
        public string Icon { get; set; }
        public string CodeModule { get; set; }
        public int IdParent { get; set; }
        public int Priority { get; set; }
    }
}
