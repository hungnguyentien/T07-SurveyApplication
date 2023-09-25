using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SurveyApplication.Utility.Enums
{
    public static class EnumBackupStore
    {
        public enum ScheduleDayofweek
        {
            [Description("Hằng ngày")]
            HangNgay = 0,
            [Description("Thứ 2")]
            Thu2 = 2,
            [Description("Thứ 3")]
            Thu3 = 4,
            [Description("Thứ 4")]
            Thu4 = 8,
            [Description("Thứ 5")]
            Thu5 = 16,
            [Description("Thứ 6")]
            Thu6 = 32,
            [Description("Thứ 7")]
            Thu7 = 64,
            [Description("Chủ nhật")]
            CN = 1

        }

        public enum ScheduleHour
        {
            [Description("00")]
            Gio0 = 0,
            [Description("01")]
            Gio1 = 1,
            [Description("02")]
            Gio2 = 2,
            [Description("03")]
            Gio3 = 3,
            [Description("04")]
            Gio4 = 4,
            [Description("05")]
            Gio5 = 5,
            [Description("06")]
            Gio6 = 6,
            [Description("07")]
            Gio7 = 7,
            [Description("08")]
            Gio8 = 8,
            [Description("09")]
            Gio9 = 9,
            [Description("10")]
            Gio10 = 10,
            [Description("11")]
            Gio11 = 11,
            [Description("12")]
            Gio12 = 12,
            [Description("13")]
            Gio13 = 13,
            [Description("14")]
            Gio14 = 14,
            [Description("15")]
            Gio15 = 15,
            [Description("16")]
            Gio16 = 16,
            [Description("17")]
            Gio17 = 17,
            [Description("18")]
            Gio18 = 18,
            [Description("19")]
            Gio19 = 19,
            [Description("20")]
            Gio20 = 20,
            [Description("21")]
            Gio21 = 21


        }

        public enum ScheduleMinute
        {
            [Description("00")]
            Minute0 = 0,
            [Description("05")]
            Minute5 = 5,
            [Description("10")]
            Minute10 = 10,
            [Description("15")]
            Minute15 = 15,
            [Description("20")]
            Minute20 = 20,
            [Description("25")]
            Minute25 = 25,
            [Description("30")]
            Minute30 = 30,
            [Description("35")]
            Minute35 = 35,
            [Description("40")]
            Minute40 = 40,
            [Description("45")]
            Minute45 = 45,
            [Description("50")]
            Minute50 = 50,
            [Description("55")]
            Minute55 = 55
        }
    }
}
