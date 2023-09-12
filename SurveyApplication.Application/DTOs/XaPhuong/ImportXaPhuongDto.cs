using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.XaPhuong
{
    public class ImportXaPhuongDto
    {
        public IFormFile? File { get; set; }
    }
}
