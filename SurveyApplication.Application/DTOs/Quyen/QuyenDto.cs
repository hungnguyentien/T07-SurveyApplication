using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.Quyen
{
    public partial class QuyenDto : BaseDto
    {
        public int MaQuyen { get; set; }
        public string? TenQuyen { get; set; }
    }
}
