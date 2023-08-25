using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public class CauHoi : BaseDomainEntity
    {
        [Required]
        public string MaCauHoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public bool? BatBuoc { get; set; }
        [Required]
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public int SoLuongFileToiDa { get; set; }
        public int KichThuocFile { get; set; }
         public bool? IsOther { get; set; }
        public string LabelCauTraLoi { get; set; }
    }
}
