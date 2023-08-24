using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class NguoiDung : BaseDomainEntity
    {

        [Required]
        public int MaNguoiDung { get; set; }


        [Required]
        public string? TenNguoiDung { get; set; }


        [Required]
        public string? PassWord { get;set; }


        [Required]
        public string? Email { get; set; }
    }
}
