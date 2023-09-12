﻿using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public class QuanHuyen : BaseDomainEntity
    {
        [MaxLength(250)]
        public string Code { get; set; }
        public int ParentCode { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Type { get; set; }
    }
}
