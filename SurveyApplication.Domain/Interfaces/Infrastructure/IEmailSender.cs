using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Domain.Interfaces.Infrastructure;

public interface IEmailSender
{
    Task<EmailRespose> SendEmail(string body, string? title, string? toEmail, IList<byte[]?>? lstAttachment = null,
        IList<string>? lstAttachmentName = null);

    Task<EmailRespose> SendEmailOutlook(string body, string? title, string? toEmail,
        IList<byte[]?>? lstAttachment = null,
        IList<string>? lstAttachmentName = null);

    Task<EmailRespose> ReSendEmailOutlook(int pageSize);
}