using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class DonVi : BaseDomainEntity
    {
        public Guid Madonvi { get; set; }
        public string? Maloaihinh { get; set; }
        public Guid? Malinhvuc { get; set; }
        public string? Tendonvi { get; set; }
        public string? Diachi { get; set; }
        public string? Masothue { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Sodienthoai { get; set; }
    }
}
