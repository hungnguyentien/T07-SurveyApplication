using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.VaiTroQuyen
{
    public partial class VaiTroQuyenDto : BaseDto
    {
        public int MaVaiTroQuyen { get; set; }
        public int? MaVaiTro { get; set; }
        public int? MaQuyen { get; set; }
    }
}
