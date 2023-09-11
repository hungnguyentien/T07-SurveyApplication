namespace SurveyApplication.Application.DTOs.CauHoi;

public class PhieuKhaoSatDto
{
    public int IdBangKhaoSat { get; set; }
    public List<CauHoiDto> LstCauHoi { get; set; }
    public string KqSurvey { get; set; }
    public int TrangThaiKhaoSat { get; set; }
    public int TrangThaiKq { get; set; }
}

public class EmailThongTinChungDto
{
    public int? IdGuiEmail { get; set; }
}