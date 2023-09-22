using SurveyApplication.Application.DTOs.DonVi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi
{
    public class ListTinhTp
    {
        public int? IdTinhTp { get; set; }
        public int? CountTinhTp { get; set; }
        public string? TenTinhTp { get; set; }
        public List<DonViTinhTpDto>? ListDonVi { get; set; }
    }

    public class DonViTinhTpDto
    {
        public int? Id { get; set; }
        public int? IdTinhTp { get; set; }
        public string? TinhTp { get; set; }
    }
}
