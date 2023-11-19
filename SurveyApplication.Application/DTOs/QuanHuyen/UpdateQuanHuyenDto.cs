using SurveyApplication.Application.DTOs.Common;

namespace SurveyApplication.Application.DTOs.QuanHuyen;

public class UpdateQuanHuyenDto : BaseDto, IQuanHuyenDto
{
    public string Code { get; set; }
    public string ParentCode { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}