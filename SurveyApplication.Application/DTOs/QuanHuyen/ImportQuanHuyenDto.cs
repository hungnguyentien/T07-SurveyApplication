using Microsoft.AspNetCore.Http;

namespace SurveyApplication.Application.DTOs.QuanHuyen;

public class ImportQuanHuyenDto
{
    public IFormFile? File { get; set; }
}