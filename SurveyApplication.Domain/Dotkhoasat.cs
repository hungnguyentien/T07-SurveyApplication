using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class Dotkhaosat : BaseDomainEntity
    {
        public Guid Madotkhaosat { get; set; }
        public string? Tendotkhaosat { get; set; }
        public string? Maloaihinh { get; set; }
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
    }
}
