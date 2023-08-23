using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class NguoiDungVaiTro : BaseDomainEntity
    {
        public int MaNguoiDungVaiTro { get; set; }
        public int? MaNguoiDung { get; set; }
        public int? MaVaiTro { get;set; }

    }
}
