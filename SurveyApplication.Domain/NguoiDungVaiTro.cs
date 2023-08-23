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

        [Required]
        public int MaNguoiDungVaiTro { get; set; }


        [Required]
        public int? MaNguoiDung { get; set; }

        [Required]
        public int? MaVaiTro { get;set; }

    }
}
