using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class Bangkhaosat : BaseDomainEntity
    {
        public string Mabangkhaosat { get; set; } = null!;
        public string? Maloaihinh { get; set; }
        public string? Tenbangkhaosat { get; set; }
        public int? Madotkhaosat { get; set; }
        public string? Mota { get; set; }
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
    }
}
