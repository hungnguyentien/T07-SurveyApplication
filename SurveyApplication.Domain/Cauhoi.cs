using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class Cauhoi : BaseDomainEntity
    {
        public string Macauhoi { get; set; } = null!;
        public Guid? Maloaicauhoi { get; set; }
        public bool? Batbuoc { get; set; }
    }
}
