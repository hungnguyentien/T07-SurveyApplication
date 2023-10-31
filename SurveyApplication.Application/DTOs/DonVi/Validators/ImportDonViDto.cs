using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.DonVi.Validators
{
    public class ImportDonViDto
    {
        public IFormFile? File { get; set; }
    }
}
