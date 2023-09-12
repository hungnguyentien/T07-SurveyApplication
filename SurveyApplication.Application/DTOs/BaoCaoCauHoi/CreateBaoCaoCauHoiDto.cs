namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi
{
    public class CreateBaoCaoCauHoiDto
    {
        public int IdCauHoi { get; set; }
        public int IdDotKhaoSat { get; set; }
        public int IdBangKhaoSat { get; set; }
        public int IdLoaiHinhDonVi { get; set; }
        public string? MaCauHoi { get; set; }
        public string? CauHoi { get; set; }
        public string? MaCauHoiPhu { get; set; }
        public string? CauHoiPhu { get; set; }
        public string? MaCauTraLoi { get; set; }
        public string? CauTraLoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public string? TenDaiDienCq { get; set; }
        public DateTime? DauThoiGian { get; set; }
    }
}
