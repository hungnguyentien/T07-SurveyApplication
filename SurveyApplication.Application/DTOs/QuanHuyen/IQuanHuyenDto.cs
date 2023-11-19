namespace SurveyApplication.Application.DTOs.QuanHuyen;

public interface IQuanHuyenDto
{
    public string Code { get; set; }
    public string ParentCode { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}