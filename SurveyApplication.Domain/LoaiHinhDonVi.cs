using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Domain
{
    public partial class LoaiHinhDonVi : BaseDomainEntity
    {
        [Required(ErrorMessage = "Mã loại hình đơn vị không được để trống")]
        public string MaLoaiHinh { get; set; } = null!;

        [Required(ErrorMessage = "Tên loại hình đơn vị không được để trống")]
        public string? TenLoaiHinh { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string? MoTa { get; set; }
    }
}
