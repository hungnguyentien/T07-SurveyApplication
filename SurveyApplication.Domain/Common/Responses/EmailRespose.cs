namespace SurveyApplication.Domain.Common.Responses;

public class EmailRespose
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object Trace { get; set; }
}