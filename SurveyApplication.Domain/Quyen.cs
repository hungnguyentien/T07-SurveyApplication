using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class Quyen : BaseDomainEntity
    {

        [Required(ErrorMessage = "Mã quyền không được để trống")]
        public int MaQuyen { get; set; }


        [Required(ErrorMessage = "Tên quyền không được để trống")]
        public string? TenQuyen { get; set; }
    }
}
