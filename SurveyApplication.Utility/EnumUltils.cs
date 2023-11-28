using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SurveyApplication.Utility
{
    public static class EnumUltils
    {
        public static Dictionary<T, string> GetDescription<T>() where T : Enum
        {
            var rs = new Dictionary<T, string>();
            var enumType = typeof(T);
            var enumValues = enumType.GetEnumValues();
            try
            {
                foreach (T item in enumValues)
                {
                    var memberInfo = enumType.GetMember(item.ToString()).First();
                    var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                    if (descriptionAttribute != null)
                        rs.Add(item, descriptionAttribute.Description);
                    else
                        rs.Add(item, item.ToString());
                }
            }
            catch (Exception)
            {
            }

            return rs;
        }

        public static Dictionary<int, string> GetDescriptionValue<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T is not an Enum type");

            var enumType = typeof(T);
            return Enum.GetValues(typeof(T))
                .Cast<object>()
                .ToDictionary(k => (int)k, v => enumType.GetMember(v.ToString() ?? string.Empty).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? v.ToString());
        }
    }
}