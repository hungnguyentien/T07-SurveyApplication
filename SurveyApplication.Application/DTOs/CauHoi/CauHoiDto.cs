using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.CauHoi
{
    public class CauHoiDto : BaseDto
    {
        /// <summary>
        /// type
        /// </summary>
        public int LoaiCauHoi { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string MaCauHoi { get; set; }
        /// <summary>
        /// isRequired
        /// </summary>
        public bool? BatBuoc { get; set; }
        /// <summary>
        /// title
        /// </summary>
        public string TieuDe { get; set; }
        /// <summary>
        /// showOtherItem
        /// </summary>
        public bool? IsOther { get; set; }
        /// <summary>
        /// otherText
        /// </summary>
        public string LabelCauTraLoi { get; set; }
        /// <summary>
        /// maxSize
        /// </summary>
        public int KichThuocFile { get; set; }
        [NotMapped]
        public List<CotDto>? LstCot { get; set; }
        [NotMapped]
        public List<HangDto>? LstHang { get; set; }
    }
}
