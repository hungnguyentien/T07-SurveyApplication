using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.XaPhuong
{
    public class CreateXaPhuongDto :  IXaPhuongDto
    {
        public string Code { get; set; }
        public int ParentCode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
