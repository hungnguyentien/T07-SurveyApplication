using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDungVaiTro
{
    public partial class NguoiDungVaiTroDto :BaseDto
    {
        public int MaNguoiDungVaiTro { get; set; }
        public int? MaNguoiDung { get; set; }
        public int? MaVaiTro { get; set; }
    }
}
