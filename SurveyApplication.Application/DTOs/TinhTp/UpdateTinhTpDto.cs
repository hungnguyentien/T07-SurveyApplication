using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.TinhTp;

public class UpdateTinhTpDto : BaseDto, ITinhTpDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}