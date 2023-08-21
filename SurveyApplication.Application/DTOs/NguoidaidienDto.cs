using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Application.DTOs
{
    public partial class NguoidaidienDto : BaseDto
    {
        public long Manguoidaidien { get; set; }
        public string? Hoten { get; set; }
        public long? Madonvi { get; set; }
        public string? Chucvu { get; set; }
        public string? Sodienthoai { get; set; }
        public string? Email { get; set; }
        public string? Mota { get; set; }
        public int? ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
