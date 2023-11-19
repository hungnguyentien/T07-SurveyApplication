namespace SurveyApplication.Application.DTOs.QuanHuyen;

public class CreateQuanHuyenDto : IQuanHuyenDto
{
    public string Code { get; set; }
    public string ParentCode { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}