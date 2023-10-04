using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.PhieuKhaoSat
{
    public class UploadFileDto
    {
        public List<IFormFile>? files { get; set; }
    }
}
