using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Domain
{
    public partial class LoaiHinhDonVi : BaseDomainEntity
    {
        [Required]
        [MaxLength(250)]
        public string MaLoaiHinh { get; set; } = null!;

        [Required]
        public string TenLoaiHinh { get; set; }

        [Required]
        public string MoTa { get; set; }
    }
}
