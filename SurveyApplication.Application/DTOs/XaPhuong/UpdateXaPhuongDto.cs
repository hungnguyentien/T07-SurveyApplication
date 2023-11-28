using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.DTOs.XaPhuong
{
    public class UpdateXaPhuongDto : BaseDto, IXaPhuongDto
    {
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
