namespace SurveyApplication.Application.DTOs.PhieuKhaoSat
{
    public class CreateKetQuaDto: IKetQuaDto
    {
        public int IdDonVi { get; set; }
        public int IdNguoiDaiDien { get; set; }
        public int IdBangKhaoSat { get; set; }
        public string Data { get; set; }
        public int TrangThai { get; set; }
    }
}
