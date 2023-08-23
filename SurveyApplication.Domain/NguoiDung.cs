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

        [Required(ErrorMessage = "Mã người dùng không được để trống")]
        public int MaNguoiDung { get; set; }


        [Required(ErrorMessage = "Tên người dùng không được để trống")]
        public string? TenNguoiDung { get; set; }


        [Required(ErrorMessage = "Password không được để trống")]
        public string? PassWord { get;set; }


        [Required(ErrorMessage = "Email không được để trống")]
        public string? Email { get; set; }
    }
}
