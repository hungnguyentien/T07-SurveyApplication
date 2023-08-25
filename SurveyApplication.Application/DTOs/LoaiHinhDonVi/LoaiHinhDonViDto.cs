using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi
{
    public partial class LoaiHinhDonViDto : BaseDto
    {
        public string MaLoaiHinh { get; set; } = null!;
        public string? TenLoaiHinh { get; set; }
        public string? MoTa { get; set; }
    }
}
