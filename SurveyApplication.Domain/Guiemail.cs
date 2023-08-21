using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class Guiemail : BaseDomainEntity
    {
        public Guid Maguiemail { get; set; }
        public string? Diachinhan { get; set; }
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
    }
}
