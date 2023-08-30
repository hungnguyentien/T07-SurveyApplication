using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Utils
{
    public class EnumUltils
    {
        public static Dictionary<T, string> GetDescription<T>() where T : Enum
        {
            var rs = new Dictionary<T, string>();
            Type enumType = typeof(T);
            var enumValues = enumType.GetEnumValues();
            try
            {
                foreach (T item in enumValues)
                {
                    MemberInfo memberInfo = enumType.GetMember(item.ToString()).First();
                    var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                    if (descriptionAttribute != null)
                    {
                        rs.Add(item, descriptionAttribute.Description);
                    }
                    else
                    {
                        rs.Add(item, item.ToString());
                    }
                }
            }
            catch (Exception)
            {

            }

            return rs;
        }
    }
}
