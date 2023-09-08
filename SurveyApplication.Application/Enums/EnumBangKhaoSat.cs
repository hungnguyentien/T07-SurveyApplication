using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Enums
{
    public static class EnumBangKhaoSat
    {
        public enum TrangThai
        {
            [Description("Chờ khảo sát")]
            ChoKhaoSat = 0,
            [Description("Đang khảo sát")]
            DangKhaoSat = 1,
            [Description("Hoàn thành")]
            HoanThanh = 2,
            [Description("Tạm dừng")]
            TamDung = 3,
        }
    }
}
