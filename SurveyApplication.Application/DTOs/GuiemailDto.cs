using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Application.DTOs
{
    public partial class GuiemailDto : BaseDto
    {
        public long Maguiemail { get; set; }
        public string? Diachinhan { get; set; }
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
        public int? ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
