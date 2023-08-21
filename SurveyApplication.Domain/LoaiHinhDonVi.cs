using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class LoaiHinhDonVi : BaseDomainEntity
    {
        public string MaLoaiHinh { get; set; } = null!;
        public string? TenLoaiHinh { get; set; }
        public string? MoTa { get; set; }
    }
}
