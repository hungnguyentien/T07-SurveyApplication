using System.ComponentModel;

namespace SurveyApplication.Application.Enums
{
    public class EnumGuiEmail
    {
        public enum TrangThai
        {
            [Description("Gửi lỗi")]
            GuiLoi = 0,
            [Description("Đã gửi")]
            DaGui = 1,
            [Description("Thu hồi")]
            ThuHoi = 2,
            [Description("Đang gửi")]
            DangGui = 3
        }
    }
}
