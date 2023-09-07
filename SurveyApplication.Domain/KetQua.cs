using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain
{
    public class KetQua: BaseDomainEntity
    {

        public string Data { get; set; }
        //public int IdDonVi { get; set; }
        //public int IdNguoiDaiDien { get; set; }
        //public int IdBangKhaoSat { get; set; }
        public int IdGuiEmail { get; set; }
        /// <summary>
        /// 0 vừa gửi mail, 1 lưu nháp, 2 hoàn thành
        /// </summary>
        public int TrangThai { get; set; }
    }
}
