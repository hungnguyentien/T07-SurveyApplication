using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class Linhvuchoatdong : BaseDomainEntity
    {
        public Guid Malinhvuc { get; set; }
        public string? Tenlinhvuc { get; set; }
    }
}
