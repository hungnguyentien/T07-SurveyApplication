using System.ComponentModel;

namespace SurveyApplication.Utility.Enums
{
    public static class EnumKetQua
    {
        public enum TrangThai
        {
            [Description("Chưa lưu")] ChuaLuu = 0,
            [Description("Lưu nháp")] LuuNhap = 1,
            [Description("Hoàn thành")] HoanThanh = 2
        }
    }
}