using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Application.DTOs
{
    public partial class LinhvuchoatdongDto : BaseDto
    {
        public long Malinhvuc { get; set; }
        public string? Tenlinhvuc { get; set; }
        public int? ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
