using Microsoft.AspNetCore.Http;

namespace SurveyApplication.Application.DTOs.DonVi.Validators;

public class ImportDonViDto
{
    public IFormFile? File { get; set; }
}