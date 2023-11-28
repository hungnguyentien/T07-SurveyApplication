namespace SurveyApplication.Domain.Common.Responses;

public class BaseCommandResponse
{
    public BaseCommandResponse()
    {
    }

    public BaseCommandResponse(string message)
    {
        Errors = new List<string>();
        Message = message;
        Success = true;
    }

    public BaseCommandResponse(string message, List<string> errors, bool success)
    {
        Errors = errors;
        Message = message;
        Success = success;
    }

    public int Id { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}