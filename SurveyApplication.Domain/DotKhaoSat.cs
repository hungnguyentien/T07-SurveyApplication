using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class DotKhaoSat : BaseDomainEntity
    {
        public string MaDotKhaoSat { get; set; }
        public string? MaLoaiHinh { get; set; }
        public string? TenDotKhaoSat { get; set; }
         public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuuc { get; set; }

        
    }
}
