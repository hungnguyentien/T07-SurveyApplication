namespace SurveyApplication.Application.DTOs.TinhTp;

public class CreateTinhTpDto : ITinhTpDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}