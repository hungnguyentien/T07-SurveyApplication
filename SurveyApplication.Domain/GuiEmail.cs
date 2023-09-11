using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class GuiEmail : BaseDomainEntity
{
    [Required] [MaxLength(250)] public string MaGuiEmail { get; set; }

    [Required] public string DiaChiNhan { get; set; }

    [Required] public string TieuDe { get; set; }

    [Required] public string NoiDung { get; set; }

    public int IdBangKhaoSat { get; set; }
    public int IdDonVi { get; set; }
    public int TrangThai { get; set; }
    public DateTime ThoiGian { get; set; }
}