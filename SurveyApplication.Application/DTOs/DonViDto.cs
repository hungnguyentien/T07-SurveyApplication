using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Application.DTOs
{
    public partial class DonViDto : BaseDto
    {
        public long Madonvi { get; set; }
        public string? Maloaihinh { get; set; }
        public long? Malinhvuc { get; set; }
        public string? Tendonvi { get; set; }
        public string? Diachi { get; set; }
        public string? Masothue { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Sodienthoai { get; set; }
        public int? ActiveFlag { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
