using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.Module;

public class ModuleDto : BaseDto
{
    public string? Name { get; set; }
    public string? RouterLink { get; set; }
    public string? Icon { get; set; }
    public string? CodeModule { get; set; }
    public int? IdParent { get; set; }
    public int? Priority { get; set; }
}