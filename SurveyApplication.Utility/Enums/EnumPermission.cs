using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SurveyApplication.Utility.Enums
{
    public class EnumPermission
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
