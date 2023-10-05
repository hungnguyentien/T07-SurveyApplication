using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class LoaiHinhDonVi : BaseDomainEntity
{
    [Required] [MaxLength(250)] public string MaLoaiHinh { get; set; } = null!;

    [Required] public string TenLoaiHinh { get; set; }

    public string? MoTa { get; set; }
}