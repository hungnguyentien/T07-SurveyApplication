using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class CauHoi : BaseDomainEntity
    {
        public string MaCauHoi { get;set; }
        public string? MaLoaiCauHoi { get;set; }
        public bool? BatBuoc {  get;set; }
    }
}
