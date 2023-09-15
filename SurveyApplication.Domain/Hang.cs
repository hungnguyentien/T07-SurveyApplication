using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class Hang : BaseDomainEntity
{
    [Required] [MaxLength(250)] public string MaHang { get; set; }

    [Required] [MaxLength(500)] public string NoiDung { get; set; }

    public int IdCauHoi { get; set; }
}