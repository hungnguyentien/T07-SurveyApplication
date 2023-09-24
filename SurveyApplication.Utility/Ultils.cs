using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApplication.Utility
{
    public static class Ultils
    {
        public static T DeepCopy<T>(T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static string ConvertCapacity(long byteCapacity)
        {
            var lstPow = new List<int> { 3, 2, 1 };
            var lstUnit = new List<string> { "Gb", "Mb", "Kb" };
            var capacity = string.Empty;
            foreach (var itemPow in lstPow.Where(itemPow => byteCapacity >= Math.Pow(1024, itemPow)))
            {
                capacity = Math.Ceiling(byteCapacity / Math.Pow(1024, itemPow)) + lstUnit.ElementAtOrDefault(lstPow.FindIndex(x => x.Equals(itemPow)));
                break;
            }

            return capacity;
        }
    }
}
