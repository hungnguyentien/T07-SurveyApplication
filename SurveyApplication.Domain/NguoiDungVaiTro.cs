using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class NguoiDungVaiTro : BaseDomainEntity
    {

        [Required(ErrorMessage = "Mã quan hệ người dùng với vai trò không được để trống")]
        public int MaNguoiDungVaiTro { get; set; }


        [Required(ErrorMessage = "Mã người dùng không được để trống")]
        public int? MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Mã vai trò không được để trống")]
        public int? MaVaiTro { get;set; }

    }
}
