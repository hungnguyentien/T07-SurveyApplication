using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.DTOs.TinhTp
{
    public class UpdateTinhTpDto : BaseDto, ITinhTpDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
