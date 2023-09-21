using System.ComponentModel;

namespace SurveyApplication.Utility.Enums
{
    public static class EnumPermission
    {
        public enum Type
        {
            [Description("Xem")]
            Read = 1,
            [Description("Tạo mới")]
            Create = 2,
            [Description("Chỉnh sửa")]
            Update = 3,
            [Description("Xóa")]
            Deleted = 4,
            [Description("Import")]
            Import = 5,
            [Description("Export")]
            Export = 6
        }
    }
}
