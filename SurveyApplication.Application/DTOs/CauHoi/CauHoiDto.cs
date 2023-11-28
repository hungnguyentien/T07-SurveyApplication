using System.ComponentModel.DataAnnotations.Schema;
using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.CauHoi;

public class CauHoiDto : BaseDto
{
    /// <summary>
    ///     type
    /// </summary>
    public int LoaiCauHoi { get; set; }

    /// <summary>
    ///     name
    /// </summary>
    public string MaCauHoi { get; set; }

    /// <summary>
    ///     title
    /// </summary>
    public string TieuDe { get; set; }

    /// <summary>
    ///     showOtherItem
    /// </summary>
    public bool? IsOther { get; set; }

    /// <summary>
    ///     otherText
    /// </summary>
    public string LabelCauTraLoi { get; set; }

    /// <summary>
    ///     maxSize
    /// </summary>
    public int KichThuocFile { get; set; }

    /// <summary>
    ///     maxSize
    /// </summary>
    public int SoLuongFileToiDa { get; set; }

    /// <summary>
    ///     isRequired
    /// </summary>
    public string Noidung { get; set; }

    [NotMapped] public bool? BatBuoc { get; set; }
    [NotMapped]
    public string PanelTitle { get; set; } = "";

    [NotMapped] public List<CotDto>? LstCot { get; set; }

    [NotMapped] public List<HangDto>? LstHang { get; set; }

    [NotMapped] public string LoaiCauHoiText { get; set; } = "";
}