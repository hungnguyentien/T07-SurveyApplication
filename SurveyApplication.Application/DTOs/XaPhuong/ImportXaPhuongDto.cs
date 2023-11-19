using Microsoft.AspNetCore.Http;

namespace SurveyApplication.Application.DTOs.XaPhuong;

public class ImportXaPhuongDto
{
    public IFormFile? File { get; set; }
}