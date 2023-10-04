using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class DonVi : BaseDomainEntity
{
    [Required][MaxLength(250)] public string MaDonVi { get; set; }

    public int IdLoaiHinh { get; set; }

    public int? IdLinhVuc { get; set; }

    public int IdTinhTp { get; set; }

    public int IdQuanHuyen { get; set; }

    public int IdXaPhuong { get; set; }

    [Required]
    public string TenDonVi { get; set; }

    public string? DiaChi { get; set; }

    public string? MaSoThue { get; set; }

    [Required] public string Email { get; set; }

    public string? WebSite { get; set; }

    [Required] public string SoDienThoai { get; set; }
}