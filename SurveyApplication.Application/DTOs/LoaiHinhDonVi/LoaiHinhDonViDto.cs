using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi
{
    public partial class LoaiHinhDonViDto : BaseDto
    {
        public string MaLoaiHinh { get; set; }
        public string? TenLoaiHinh { get; set; }
        public string? MoTa { get; set; }
    }
}
