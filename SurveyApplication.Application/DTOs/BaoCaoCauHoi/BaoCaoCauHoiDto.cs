using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.BaoCaoCauHoi
{
    public class BaoCaoCauHoiDto : BaseDto
    {
        public int IdCauHoi { get; set; }
        public int IdDotKhaoSat { get; set; }
        public int IdBangKhaoSat { get; set; }
        public int IdLoaiHinhDonVi { get; set; }
        public string MaCauHoi { get; set; } = string.Empty;
        public string CauHoi { get; set; } = string.Empty;
        public string? MaCauHoiPhu { get; set; }
        public string? CauHoiPhu { get; set; }
        public string? MaCauTraLoi { get; set; }
        public string? CauTraLoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public string TenDaiDienCq { get; set; } = string.Empty;
        public DateTime? DauThoiGian { get; set; }
    }

    public class BaoCaoCauTraLoiDto
    {
        public int IdCauHoi { get; set; }
        public int IdDotKhaoSat { get; set; }
        public int IdBangKhaoSat { get; set; }
        public int IdLoaiHinhDonVi { get; set; }
        public string MaCauHoi { get; set; } = string.Empty;
        public string CauHoi { get; set; } = string.Empty;
        public string? MaCauHoiPhu { get; set; }
        public string? CauHoiPhu { get; set; }
        public string? MaCauTraLoi { get; set; }
        public short LoaiCauHoi { get; set; }
        public string TenDaiDienCq { get; set; } = string.Empty;
        /// <summary>
        /// Id gửi email
        /// </summary>
        public int IdGuiEmail { get; set; }
        /// <summary>
        /// Câu trả lời khác
        /// </summary>
        public bool IsOther { get; set; }
        /// <summary>
        /// Title câu trả lời khác
        /// </summary>
        public string LabelCauTraLoi { get; set; }
    }

    public class BaoCaoCauHoiKetQuaDto : BaoCaoCauTraLoiDto
    {
        public Dictionary<string, object>? DataKq { get; set; }
        public DateTime? DauThoiGian { get; set; }
    }
}
