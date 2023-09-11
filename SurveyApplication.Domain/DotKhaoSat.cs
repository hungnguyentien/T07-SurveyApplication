using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class DotKhaoSat : BaseDomainEntity
{
    [Required] [MaxLength(250)] public string MaDotKhaoSat { get; set; }

    public int IdLoaiHinh { get; set; }

    [Required] public string TenDotKhaoSat { get; set; }

    [Required] public DateTime NgayBatDau { get; set; }

    [Required] public DateTime NgayKetThuc { get; set; }

    public int? TrangThai { get; set; }
}