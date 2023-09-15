using System.ComponentModel;

namespace SurveyApplication.Application.Enums;

public static class EnumDotKhaoSat
{
    public enum TrangThai
    {
        [Description("Chờ khảo sát")] ChoKhaoSat = 0,
        [Description("Đang khảo sát")] DangKhaoSat = 1,
        [Description("Hoàn thành")] HoanThanh = 2
    }
}