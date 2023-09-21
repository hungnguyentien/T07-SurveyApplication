using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi
{
    public class ListCauHoiTraLoi
    {
        public int? IdCauHoi { get; set; }
        public string? TenCauHoi { get; set; }
        public DateTime? DauThoiGian { get; set; }
        public List<BaoCaoCauHoiDto>? CauHoiTraLoi { get; set; }
    }
}
