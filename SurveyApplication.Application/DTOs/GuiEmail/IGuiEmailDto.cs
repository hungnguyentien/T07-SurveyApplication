namespace SurveyApplication.Application.DTOs.GuiEmail;

public interface IGuiEmailDto
{
    public List<int> LstBangKhaoSat { get; set; }
    public List<int> LstIdDonVi { get; set; }
    public string TieuDe { get; set; }
    public string NoiDung { get; set; }
}