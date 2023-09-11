using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class DonVi : BaseDomainEntity
{
    [Required] [MaxLength(250)] public string MaDonVi { get; set; }

    public int IdLoaiHinh { get; set; }

    public int IdLinhVuc { get; set; }

    [Required] public string TenDonVi { get; set; }

    [Required] public string DiaChi { get; set; }

    [Required] public string MaSoThue { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string WebSite { get; set; }

    [Required] public string SoDienThoai { get; set; }
}