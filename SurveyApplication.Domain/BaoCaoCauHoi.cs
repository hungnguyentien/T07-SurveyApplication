using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain
{
    public class BaoCaoCauHoi : BaseDomainEntity
    {
        public int IdCauHoi { get; set; }
        public int IdGuiEmail { get; set; }
        public int IdDotKhaoSat { get; set; }
        public int IdBangKhaoSat { get; set; }
        public int IdDonVi { get; set; }
        public int IdLoaiHinhDonVi { get; set; }
        public string MaCauHoi { get; set; } = string.Empty;
        public string CauHoi { get; set; } = string.Empty;
        public string? MaCauHoiPhu { get; set; }
        public string? CauHoiPhu { get; set; }
        public string? MaCauTraLoi { get; set; }
        public string? CauTraLoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public string TenDaiDienCq { get; set; } = string.Empty;
        public DateTime? DauThoiGian { get; set; }
    }
}
