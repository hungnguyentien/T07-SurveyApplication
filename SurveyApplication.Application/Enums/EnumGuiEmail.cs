using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Enums
{
    public static class EnumGuiEmail
    {
        public enum TrangThai
        {
            [Description("Thành công")]
            ThanhCong = 0,
            [Description("Gửi lỗi")]
            GuiLoi = 1,
            [Description("Thu hồi")]
            ThuHoi = 2,
        }
    }
}
