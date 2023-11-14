using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Infrastructure;

namespace SurveyApplication.Infrastructure.Mail;

public class EmailSender : IEmailSender
{
    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        EmailSettings = emailSettings.Value;
    }

    private EmailSettings EmailSettings { get; }

    /// <summary>
    ///     Demo gửi email
    /// </summary>
    /// <param name="body"></param>
    /// <param name="title"></param>
    /// <param name="toEmail"></param>
    /// <param name="lstAttachment"></param>
    /// <param name="lstAttachmentName"></param>
    /// <returns></returns>
    public async Task<EmailRespose> SendEmail(string body, string? title, string? toEmail, IList<byte[]?>? lstAttachment = null,
        IList<string>? lstAttachmentName = null)
    {
        var result = new EmailRespose();
        try
        {
            await Task.Run(() =>
            {
                var userEmail = EmailSettings.Username;
                var passwordEmail = EmailSettings.Password;
                if (toEmail != null)
                {
                    var mailMessage = new MailMessage(userEmail, toEmail, title, body)
                    {
                        IsBodyHtml = true
                    };

                    //Add attachment
                    if (lstAttachment != null)
                    {
                        foreach (var attachment in lstAttachment)
                        {
                            var index = lstAttachment.ToList().FindIndex(x => x == attachment);
                            var attachmentName = lstAttachmentName?.ElementAt(index);
                            if (attachment != null)
                            {
                                var fileName = string.IsNullOrWhiteSpace(attachmentName) ? "FileDefault" : attachmentName;
                                var stream = new MemoryStream(attachment);
                                mailMessage.Attachments.Add(new Attachment(stream, fileName));

                            }
                        }
                    }

                    var netCred = new NetworkCredential(userEmail, passwordEmail);
                    var smtpClient = new SmtpClient(EmailSettings.Host, EmailSettings.Port);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = netCred;
                    smtpClient.Timeout = 200000;
                    smtpClient.Send(mailMessage);
                }

                result.IsSuccess = true;
                result.Message = "Gửi mail thành công";
            });
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
            result.Trace = ex.StackTrace ?? "";
        }

        return result;
    }
}