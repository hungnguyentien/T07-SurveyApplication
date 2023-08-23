using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.VaiTro
{
    public partial class VaiTroDto : BaseDto
    {
        public int MaVaiTro { get; set; }
        public int? TenVaiTro { get; set; }
    }
}
