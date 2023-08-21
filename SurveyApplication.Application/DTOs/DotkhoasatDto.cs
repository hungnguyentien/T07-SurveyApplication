using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Application.DTOs
{
    public partial class DotkhaosatDto : BaseDto
    {
        public int Madotkhaosat { get; set; }
        public string? Tendotkhaosat { get; set; }
        public string? Maloaihinh { get; set; }
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
        public int? ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
