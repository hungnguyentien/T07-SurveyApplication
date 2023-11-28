using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain.Common.Configurations
{
    public class StorageConfigs
    {
        public bool UseExternalStorage { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseStorage { get; set; }
    }
}
