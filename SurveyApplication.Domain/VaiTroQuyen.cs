using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class VaiTroQuyen : BaseDomainEntity
    {

        [Required(ErrorMessage = "Mã quan hệ vai trò với quyền không được để trống")]
        public int MaVaiTroQuyen { get; set; }


        [Required(ErrorMessage = "Mã vai trò không được để trống")]
        public int? MaVaiTro { get; set; }


        [Required(ErrorMessage = "Mã quyền không được để trống")]
        public int? MaQuyen { get;set; }

    }
}
