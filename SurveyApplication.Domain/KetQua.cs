using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain
{
    public class KetQua: BaseDomainEntity
    {

        public string Data { get; set; }
        public int IdDonVi { get; set; }
        public int IdNguoiDaiDien { get; set; }
        public int IdBangKhaoSat { get; set; }
        public int IdGuiEmail { get; set; }
    }
}
