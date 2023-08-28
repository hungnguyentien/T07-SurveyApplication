using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Models.Identity
{
    public class AuthRequest
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
