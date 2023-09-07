namespace SurveyApplication.Application.DTOs.PhieuKhaoSat
{
    public class CreateKetQuaDto: IKetQuaDto
    {
        public string GuiEmail { get; set; }
        public string Data { get; set; }
        public int TrangThai { get; set; }
    }
}
