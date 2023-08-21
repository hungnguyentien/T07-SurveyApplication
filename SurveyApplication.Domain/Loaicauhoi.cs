using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class Loaicauhoi : BaseDomainEntity
    {
        public Guid Maloaicauhoi { get; set; }
        public string? Tenloaicauhoi { get; set; }
    }
}
