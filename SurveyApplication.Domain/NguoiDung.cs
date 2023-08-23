using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public partial class NguoiDung : BaseDomainEntity
    {
        public int MaNguoiDung { get; set; }    
        public string? TenNguoiDung { get; set; }
        public string? PassWord { get;set; }
        public string? Email { get; set; }
    }
}
