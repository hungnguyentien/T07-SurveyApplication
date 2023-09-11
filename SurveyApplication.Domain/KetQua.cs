using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class KetQua : BaseDomainEntity
{
    public string Data { get; set; }

    public int IdGuiEmail { get; set; }

    /// <summary>
    ///     0 vừa gửi mail, 1 lưu nháp, 2 hoàn thành
    /// </summary>
    public int TrangThai { get; set; }
    /// <summary>
    /// Thời gian hoàn thành
    /// </summary>
    public DateTime? DauThoiGian { get; set; }
}