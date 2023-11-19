namespace SurveyApplication.Application.DTOs.Common;

public class BasePagingDto
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? Keyword { get; set; }
    public string? OrderBy { get; set; }
}