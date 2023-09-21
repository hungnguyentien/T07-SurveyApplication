using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi
{
    public class BaoCaoCauHoiDto : BaseDto
    {
        public int IdCauHoi { get; set; }
        public int IdDotKhaoSat { get; set; }
        public int IdBangKhaoSat { get; set; }
        public int IdDonVi { get; set; }
        public int IdLoaiHinhDonVi { get; set; }
        public string? TenLoaiHinhDonVi { get; set; }
        public string MaCauHoi { get; set; } = string.Empty;
        public string CauHoi { get; set; } = string.Empty;
        public string? MaCauHoiPhu { get; set; }
        public string? CauHoiPhu { get; set; }
        public string? MaCauTraLoi { get; set; }
        public string? CauTraLoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public string TenDaiDienCq { get; set; } = string.Empty;
        public DateTime? DauThoiGian { get; set; }

        public int CountDonViMoi { get; set; }
        public int CountDonViTraLoi { get; set; }
        public int CountDonViSo { get; set; }
        public int CountDonViBo { get; set; }
        public int CountDonViNganh { get; set; }
        public string? DiaChi { get; set; }

        public List<ListCauHoiTraLoi>? ListCauHoiTraLoi { get; set; }
    }
}
