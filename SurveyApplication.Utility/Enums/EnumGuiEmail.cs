using System.ComponentModel;

namespace SurveyApplication.Utility.Enums
{
    public static class EnumGuiEmail
    {
        public enum TrangThai
        {
            [Description("Thành công")] ThanhCong = 0,
            [Description("Gửi lỗi")] GuiLoi = 1,
            [Description("Thu hồi")] ThuHoi = 2,
            [Description("Đang gửi")] DangGui = 3
        }
    }
};

