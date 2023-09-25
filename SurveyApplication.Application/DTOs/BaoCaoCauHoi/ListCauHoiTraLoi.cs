namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi
{
    public class ListCauHoiTraLoi
    {
        public int? IdCauHoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public string? TenCauHoi { get; set; }
        public DateTime? DauThoiGian { get; set; }
        public List<CauHoiTraLoi>? CauHoiTraLoi { get; set; }
    }

    public class CauHoiTraLoi
    {
        public int IdCauHoi { get; set; }
        public int IdDotKhaoSat { get; set; }
        public int IdBangKhaoSat { get; set; }
        public int IdDonVi { get; set; }
        public int IdLoaiHinhDonVi { get; set; }
        public string? TenLoaiHinhDonVi { get; set; }
        public string? MaCauHoi { get; set; }
        public string? CauHoi { get; set; }
        public string? MaCauHoiPhu { get; set; }
        public string? CauHoiPhu { get; set; }
        public string? MaCauTraLoi { get; set; }
        public string? CauTraLoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public string? TenDaiDienCq { get; set; }
        public DateTime? DauThoiGian { get; set; }
        public string? DiaChi { get; set; }
        public int? SoLuotChon { get; set; }
        public double? TyLe { get; set; }
    }
}
