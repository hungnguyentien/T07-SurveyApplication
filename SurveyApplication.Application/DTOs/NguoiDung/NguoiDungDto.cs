using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDung
{
    public partial class NguoiDungDto : BaseDto
    {
        public int MaNguoiDung { get; set; }
        public string? TenNguoiDung { get; set; }
        public string? PassWord { get; set; }
        public string? Email { get; set; }
    }
}
