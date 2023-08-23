using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class BangKhaoSat : BaseDomainEntity
    {
        [Required(ErrorMessage = "Mã bảng khảo sát không được để trống")]
        public string MaBangKhaoSat { get; set; }

        [Required(ErrorMessage = "Mã loại hình không được để trống")]
        public int? MaLoaiHinh { get; set; }

        [Required(ErrorMessage = "Mã đợt khảo sát không được để trống")]
        public int? MaDotKhaoSat { get; set; }

        [Required(ErrorMessage = "Tên khảo sát không được để trống")]

        public string? TenBangKhaoSat { get;set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime? NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime? NgayKetThuc { get; set; }
    }
}
