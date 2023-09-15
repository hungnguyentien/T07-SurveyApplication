namespace SurveyApplication.Application.DTOs.PhieuKhaoSat;

public class CreateKetQuaDto : IKetQuaDto
{
    public int TrangThai { get; set; }
    public string GuiEmail { get; set; }
    public string Data { get; set; }
    public string IpAddressClient { get; set; }
}