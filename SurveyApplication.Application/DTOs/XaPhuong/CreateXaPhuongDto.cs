namespace SurveyApplication.Application.DTOs.XaPhuong;

public class CreateXaPhuongDto : IXaPhuongDto
{
    public string Code { get; set; }
    public string ParentCode { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}