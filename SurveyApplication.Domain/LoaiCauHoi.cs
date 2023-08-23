using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class LoaiCauHoi : BaseDomainEntity
    {
        public Guid MaLoaiCauHoi { get; set; }
        public string? TenLoaiCauHoi { get; set;}
    }
}
