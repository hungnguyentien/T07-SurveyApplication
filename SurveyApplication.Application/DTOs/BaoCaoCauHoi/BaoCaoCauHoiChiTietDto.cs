namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi;

public class BaoCaoCauHoiChiTietDto
{
    public int IdDonVi { get; set; }
    public string TenDaiDienCq { get; set; }
    public int IdBangKhaoSat { get; set; }
    public int IdDotKhaoSat { get; set; }
    public DateTime DauThoiGian { get; set; }
    public List<CauHoiCauTraLoiChiTietDto> LstCauHoiCauTraLoi { get; set; }
}

public class CauHoiCauTraLoiChiTietDto
{
    public string CauHoi { get; set; }
    public short LoaiCauHoi { get; set; }
    public List<string> CauTraLoi { get; set; }
}