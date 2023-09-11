using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.GuiEmail;

public class UpdateGuiEmailDto : BaseDto, IGuiEmailDto
{
    public string MaGuiEmail { get; set; }
    public List<int> LstBangKhaoSat { get; set; }
    public List<int> LstIdDonVi { get; set; }
    public string TieuDe { get; set; }
    public string NoiDung { get; set; }
}