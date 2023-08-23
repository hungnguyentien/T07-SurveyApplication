using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class VaiTroQuyen : BaseDomainEntity
    {
        public int MaVaiTroQuyen { get; set; }
        public int? MaVaiTro { get; set; }
        public int? MaQuyen { get;set; }

    }
}
