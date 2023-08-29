using System.ComponentModel;

namespace SurveyApplication.Application.Enums
{
    public static class EnumCauHoi
    {
        public enum Type
        {
            [Description("Một đáp án")]
            Radio = 0,
            [Description("Chọn nhiều đáp án")]
            CheckBox = 1,
            [Description("Văn bản ngắn")]
            Text = 2,
            [Description("Văn bản dài")]
            LongText = 3,
            [Description("Dạng bảng (một lựa chọn)")]
            SingleSelectMatrix = 4,
            [Description("Dạng bảng (nhiều lựa chọn)")]
            MultiSelectMatrix = 5,
            [Description("Dạng bảng (văn bản)")]
            MultiTextMatrix = 6,
            [Description("Tải tệp tin")]
            UploadFile = 7
        }

        public enum ShowOtherItem
        {
            [Description("Không")]
            No = 0,
            [Description("Có")]
            Yes = 1
        }
    }
}
