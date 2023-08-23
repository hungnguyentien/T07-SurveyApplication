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

        [Required]
        public int MaVaiTroQuyen { get; set; }


        [Required]
        public int? MaVaiTro { get; set; }


        [Required]
        public int? MaQuyen { get;set; }

    }
}
