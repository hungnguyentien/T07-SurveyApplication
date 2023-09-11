namespace SurveyApplication.Application.DTOs.Common;

public abstract class BaseDto
{
    public int? Id { get; set; }
    public int? ActiveFlag { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? Created { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime? Modified { get; set; }
}