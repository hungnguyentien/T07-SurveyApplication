using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.CauHoi
{
    public partial class CauHoiDto : BaseDto
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
    }
}
