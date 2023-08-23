using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class Quyen : BaseDomainEntity
    {
        public int MaQuyen { get; set; }
        public string? TenQuyen { get; set; }
    }
}
