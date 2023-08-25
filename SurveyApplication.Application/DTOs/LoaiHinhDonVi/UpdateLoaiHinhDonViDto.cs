using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi
{
    public class UpdateLoaiHinhDonViDto : BaseDto, ILoaiHinhDonViDto
    {
        public string MaLoaiHinh { get; set; } = null!;
        public string? TenLoaiHinh { get; set; }
        public string? MoTa { get; set; }
    }
}
