using Microsoft.AspNetCore.Http;

namespace SurveyApplication.Application.DTOs.PhieuKhaoSat;

public class UploadFileDto
{
    public List<IFormFile>? Files { get; set; }
}