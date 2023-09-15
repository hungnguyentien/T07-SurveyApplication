using System.ComponentModel;

namespace Hangfire.Application.Enums
{
    public static class EnumCommon
    {
        public enum Status
        {
            [Description("Không hiệu lực")]
            InActive = 0,
            [Description("Hiệu lực")]
            Active = 1,
        }
    }
}