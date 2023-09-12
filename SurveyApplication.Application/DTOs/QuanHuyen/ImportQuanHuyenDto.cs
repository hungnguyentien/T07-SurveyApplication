using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.QuanHuyen
{
    public class ImportQuanHuyenDto
    {
        public IFormFile? File { get; set; }
    }
}
