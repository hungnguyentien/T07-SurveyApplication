using Microsoft.AspNetCore.Http;

namespace SurveyApplication.Application.DTOs.TinhTp;

public class ImportTinhTpDto
{
    public IFormFile? File { get; set; }
}