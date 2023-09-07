using System.ComponentModel;

namespace SurveyApplication.Application.Enums
{
    public class EnumKetQua
    {
        public enum TrangThai
        {
            [Description("Lưu nháp")]
            DangKhaoSat = 1,
            [Description("Hoàn thành")]
            HoanThanh = 2
        }
    }
}
