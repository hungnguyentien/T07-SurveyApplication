using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Domain
{
    public partial class Nguoidaidien : BaseDomainEntity
    {
        public Guid Manguoidaidien { get; set; }
        public string? Hoten { get; set; }
        public Guid? Madonvi { get; set; }
        public string? Chucvu { get; set; }
        public string? Sodienthoai { get; set; }
        public string? Email { get; set; }
        public string? Mota { get; set; }
    }
}
