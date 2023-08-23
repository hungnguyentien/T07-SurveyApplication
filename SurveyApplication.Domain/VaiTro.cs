using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class VaiTro : BaseDomainEntity
    {
        public int MaVaiTro { get; set; }
        public int? TenVaiTro { get; set; }

    }
}
