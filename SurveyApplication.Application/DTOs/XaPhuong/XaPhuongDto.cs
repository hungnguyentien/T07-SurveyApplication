using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.XaPhuong
{
    public partial class XaPhuongDto : BaseDto
    {
        public string Code { get; set; }
        public string parent_code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public string NameTinhTp { get; set; }
        public string CodeQuanHuyen { get; set; }
        public string NameQuanHuyen { get; set; }
    }
}
