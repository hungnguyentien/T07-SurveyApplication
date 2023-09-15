using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class BangKhaoSat : BaseDomainEntity
{
    [Required] [MaxLength(250)] public string MaBangKhaoSat { get; set; }

    public int IdLoaiHinh { get; set; }

    public int IdDotKhaoSat { get; set; }

    [Required] public string TenBangKhaoSat { get; set; }

    [Required] public string MoTa { get; set; }

    [Required] public DateTime NgayBatDau { get; set; }

    [Required] public DateTime NgayKetThuc { get; set; }

    public int? TrangThai { get; set; }
}