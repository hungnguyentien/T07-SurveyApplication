using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class LinhVucHoatDong : BaseDomainEntity
{
    [Required] [MaxLength(250)] public string MaLinhVuc { get; set; }

    [Required] public string TenLinhVuc { get; set; }

    public string MoTa { get; set; }
}