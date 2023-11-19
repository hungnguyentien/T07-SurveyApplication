using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.XaPhuong;

public class XaPhuongDto : BaseDto
{
    public string Code { get; set; }
    public string parent_code { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public string NameTinhTp { get; set; }
    public string CodeQuanHuyen { get; set; }
    public string NameQuanHuyen { get; set; }
}