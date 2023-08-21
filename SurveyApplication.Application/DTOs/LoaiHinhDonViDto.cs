using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Application.DTOs
{
    public partial class LoaiHinhDonViDto : BaseDto
    {
        public string Maloaihinh { get; set; } = null!;
        public string? Tenloaihinh { get; set; }
        public string? Mota { get; set; }
        public int? ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
