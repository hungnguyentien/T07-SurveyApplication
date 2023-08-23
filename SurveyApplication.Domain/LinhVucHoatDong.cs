using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class LinhVucHoatDong : BaseDomainEntity
    {
           public Guid MaLinhVuc { get; set; }
        public string? TenLinhVuc { get; set; }  
    }
}
