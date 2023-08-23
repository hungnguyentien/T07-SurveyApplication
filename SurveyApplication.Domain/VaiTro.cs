using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class VaiTro : BaseDomainEntity
    {


        [Required(ErrorMessage = "Mã vai trò không được để trống")]
        public int MaVaiTro { get; set; }


        [Required(ErrorMessage = "Tên vai trò không được để trống")]
        public string? TenVaiTro { get; set; }

    }
}
