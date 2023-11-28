using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class NguoiDaiDien : BaseDomainEntity
{
    [MaxLength(250)] public string MaNguoiDaiDien { get; set; }

    public int IdDonVi { get; set; }

    [Required] public string HoTen { get; set; }

    public string? ChucVu { get; set; }

    [Required] public string SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? MoTa { get; set; }
}