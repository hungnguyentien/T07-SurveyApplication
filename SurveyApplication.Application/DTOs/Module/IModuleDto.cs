namespace SurveyApplication.Application.DTOs.Module;

public interface IModuleDto
{
    public string? Name { get; set; }
    public string? RouterLink { get; set; }
    public string? Icon { get; set; }
    public string? CodeModule { get; set; }
    public int? IdParent { get; set; }
    public int? Priority { get; set; }
}