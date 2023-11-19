using System.ComponentModel;

namespace SurveyApplication.Utility.Enums
{
    public static class EnumCommon
    {
        public enum ActiveFlag
        {
            [Description("Không hiệu lực")] InActive = 0,
            [Description("Hiệu lực")] Active = 1
        }
    }
}