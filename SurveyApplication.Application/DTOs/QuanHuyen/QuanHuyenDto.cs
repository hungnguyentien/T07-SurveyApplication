using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.QuanHuyen
{
    public partial class QuanHuyenDto : BaseDto
    {
        public string Code { get; set; }
        public int ParentCode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
