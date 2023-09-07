using System.ComponentModel;

namespace SurveyApplication.Application.Enums
{
    public static class EnumCommon
    {
       public enum ActiveFlag
       {
            [Description("Không hiệu lực")]
            InActive = 0,
            [Description("Hiệu lực")]
            Active = 1,
       }

       public enum KetQua
       {
           [Description("Đang thực hiện")]
           Processing = 0,
           [Description("Lưu nháp")]
           Temporary = 0,
           [Description("Hoàn thành")]
           Complete = 1,
       }
    }
}
