using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.CauHoi
{
    public partial class CauHoiDto : BaseDto
    {
        public string MaCauHoi { get; set; }
        public string? MaLoaiCauHoi { get; set; }
        public bool? BatBuoc { get; set; }
    }
}
