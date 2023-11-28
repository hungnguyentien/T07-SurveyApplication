using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApplication.Application.DTOs.GuiEmail;

public class CreateGuiEmailDto : IGuiEmailDto
{
    [NotMapped] public List<int> LstBangKhaoSat { get; set; }

    [NotMapped] public List<int> LstIdDonVi { get; set; }

    public string TieuDe { get; set; }
    public string NoiDung { get; set; }
}