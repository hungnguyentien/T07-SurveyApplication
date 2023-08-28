using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDung
{
    public class UpdateNguoiDungDto : BaseDto,INguoiDungDto
    {
        public int MaNguoiDung { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? HoTen { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? NgaySinh { get; set; }
    }
}
