using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class LoaiCauHoi : BaseDomainEntity
    {
        [Required(ErrorMessage = "Mã loại câu hỏi không được để trống")]
        public Guid MaLoaiCauHoi { get; set; }

        [Required(ErrorMessage = "Tên loại câu hỏi không được để trống")]
        public string? TenLoaiCauHoi { get; set;}
    }
}
