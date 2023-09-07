namespace SurveyApplication.Application.DTOs.PhieuKhaoSat
{
    public class CreateKetQuaDto: IKetQuaDto
    {
        public int IdGuiEmail { get; set; }
        public string Data { get; set; }
        public int TrangThai { get; set; }
    }
}
