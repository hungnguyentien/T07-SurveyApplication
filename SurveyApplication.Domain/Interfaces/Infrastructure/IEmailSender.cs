using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Domain.Interfaces.Infrastructure;

public interface IEmailSender
{
    Task<EmailRespose> SendEmail(string body, string? title, string? toEmail, byte[]? attachment = null,
        string attachmentName = "");
}