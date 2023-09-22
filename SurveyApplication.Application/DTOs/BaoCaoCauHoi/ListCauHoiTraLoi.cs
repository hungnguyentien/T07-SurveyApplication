namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi
{
    public class ListCauHoiTraLoi
    {
        public int? IdCauHoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public string? TenCauHoi { get; set; }
        public List<CauHoiTraLoi>? CauHoiTraLoi { get; set; }
    }

    public class CauHoiTraLoi
    {
        public string? CauTraLoi { get; set; }
        public int SoLuotChon { get; set; }
        public double TyLe { get; set; }
    }
}
