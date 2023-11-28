using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.DTOs.LinhVucHoatDong
{
    public class UpdateLinhVucHoatDongDto : BaseDto, ILinhVucHoatDongDto
    {
        public string MaLinhVuc { get; set; }
        public string? TenLinhVuc { get; set; }
        public string? MoTa { get; set; }
    }
}
