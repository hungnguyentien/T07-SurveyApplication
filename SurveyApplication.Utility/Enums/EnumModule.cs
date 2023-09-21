using System.ComponentModel;

namespace SurveyApplication.Utility.Enums
{
    public static class EnumModule
    {
        public enum Code
        {
            [Description("Dashboard")]
            Dashboard = 1,
            [Description("Quản lý loại hình đơn vị")]
            QlLhDv = 2,
            [Description("Quản lý đơn vị")]
            QlDv = 3,
            [Description("Quản lý câu hỏi")]
            QlCh = 4,
            [Description("Quản lý đợt khảo sát")]
            QlDks = 5,
            [Description("Quản lý bảng khảo sát")]
            QlKs = 6,
            [Description("Quản lý gửi email")]
            QlGm = 7,
            [Description("Thống kê báo cáo")]
            TkKs = 8,
            [Description("Quản lý lĩnh vực hoạt động")]
            LvHd = 9,
            [Description("Quản lý tỉnh thành")]
            QlTt = 10,
            [Description("Quản lý quận/huyện")]
            QlQh = 11,
            [Description("Quản lý xã/phường")]
            QlPx = 12,
            [Description("Quản lý nhóm quyền")]
            QlNq = 13,
            [Description("Quản lý tài khoản")]
            QlTk = 14,
            ///Max = 14
        }
    }
}
