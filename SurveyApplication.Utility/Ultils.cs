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
    }
}
