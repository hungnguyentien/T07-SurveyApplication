using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Models.Identity
{
    public class RegistrationRequest
    {
        public int MaNguoiDung { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? HoTen { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public DateTime? NgaySinh { get; set; }
    }
}
