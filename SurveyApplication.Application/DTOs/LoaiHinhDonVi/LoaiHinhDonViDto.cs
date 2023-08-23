using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi
{
    public partial class LoaiHinhDonViDto : BaseDto
    {
        public string Maloaihinh { get; set; } = null!;
        public string? Tenloaihinh { get; set; }
        public string? Mota { get; set; }
    }
}
