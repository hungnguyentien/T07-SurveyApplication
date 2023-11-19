using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.XaPhuong;

public class UpdateXaPhuongDto : BaseDto, IXaPhuongDto
{
    public string Code { get; set; }
    public string ParentCode { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}