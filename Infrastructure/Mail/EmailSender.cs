using System.Net;
using System.Net.Mail;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using Attachment = System.Net.Mail.Attachment;
using Task = System.Threading.Tasks.Task;

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
    public async Task<EmailRespose> SendEmail(string body, string? title, string? toEmail,
        IList<byte[]?>? lstAttachment = null,
        IList<string>? lstAttachmentName = null)
    {
        var result = new EmailRespose();
        try
        {
            await Task.Run(() =>
            {
                var userEmail = EmailSettings.Username;
                var passwordEmail = EmailSettings.Password;
                var smtpClient = new SmtpClient(EmailSettings.Host, EmailSettings.Port);
                if (toEmail != null)
                {
                    var mailMessage = new MailMessage(userEmail, toEmail, title, body)
                    {
                        IsBodyHtml = true,
                    };

                    if (!string.IsNullOrEmpty(EmailSettings.CcMail))
                        mailMessage.CC.Add(EmailSettings.CcMail);

                    //Add attachment
                    if (lstAttachment != null)
                        foreach (var attachment in lstAttachment)
                        {
                            var index = lstAttachment.ToList().FindIndex(x => x == attachment);
                            var attachmentName = lstAttachmentName?.ElementAt(index);
                            if (attachment != null)
                            {
                                var fileName = string.IsNullOrWhiteSpace(attachmentName)
                                    ? "FileDefault"
                                    : attachmentName;
                                var stream = new MemoryStream(attachment);
                                mailMessage.Attachments.Add(new Attachment(stream, fileName));
                            }
                        }

                    var netCred = new NetworkCredential(userEmail, passwordEmail);
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

    public async Task<EmailRespose> SendEmailOutlook(string body, string? title, string? toEmail,
        IList<byte[]?>? lstAttachment = null,
        IList<string>? lstAttachmentName = null)
    {
        var result = new EmailRespose();
        try
        {
            await Task.Run(() =>
            {
                var userEmail = EmailSettings.Username;
                var passwordEmail = EmailSettings.Password;
                var service = new ExchangeService(ExchangeVersion.Exchange2010_SP1)
                {
                    Credentials = new NetworkCredential(userEmail, passwordEmail)
                };
                if (toEmail != null)
                {
                    service.AutodiscoverUrl(userEmail);
                    var emailMessage = new EmailMessage(service)
                    {
                        Subject = title,
                        Body = new MessageBody(body),
                        ToRecipients = { toEmail }
                    };

                    if (!string.IsNullOrEmpty(EmailSettings.CcMail))
                        emailMessage.CcRecipients.Add(EmailSettings.CcMail);

                    //Add attachment
                    if (lstAttachment != null)
                        foreach (var attachment in lstAttachment)
                        {
                            var index = lstAttachment.ToList().FindIndex(x => x == attachment);
                            var attachmentName = lstAttachmentName?.ElementAt(index);
                            if (attachment == null) continue;
                            var fileName = string.IsNullOrWhiteSpace(attachmentName)
                                ? "FileDefault"
                                : attachmentName;
                            var stream = new MemoryStream(attachment);
                            emailMessage.Attachments.AddFileAttachment(fileName, stream);
                        }

                    emailMessage.SendAndSaveCopy();
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

    public async Task<EmailRespose> ReSendEmailOutlook(int pageSize)
    {
        var result = new EmailRespose();
        try
        {
            const int offset = 0;
            var userEmail = EmailSettings.Username;
            var passwordEmail = EmailSettings.Password;
            var service = new ExchangeService(ExchangeVersion.Exchange2010_SP1)
            {
                Credentials = new NetworkCredential(userEmail, passwordEmail)
            };
            service.AutodiscoverUrl(userEmail);
            var view = new ItemView(pageSize, offset, OffsetBasePoint.Beginning)
            {
                PropertySet = PropertySet.IdOnly
            };
            FindItemsResults<Item> findResults = await service.FindItems(WellKnownFolderName.Drafts, view);
            var emails = findResults.Items.Take(pageSize).Cast<EmailMessage>().ToList();
            foreach (var emailMessage in emails)
            {
                await emailMessage.SendAndSaveCopy();
            }

            result.IsSuccess = true;
            result.Message = "Gửi mail thành công";
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
            result.Trace = ex.StackTrace ?? "";
        }

        return result;
    }

    public async Task<List<string>> GetSendEmailOutlook()
    {
        try
        {
            const int offset = 0;
            var userEmail = EmailSettings.Username;
            var passwordEmail = EmailSettings.Password;
            var service = new ExchangeService(ExchangeVersion.Exchange2010_SP1)
            {
                Credentials = new NetworkCredential(userEmail, passwordEmail)
            };
            service.AutodiscoverUrl(userEmail);
            var view = new ItemView(1000, offset, OffsetBasePoint.Beginning)
            {
                PropertySet = PropertySet.FirstClassProperties
            };
            var findResults = await service.FindItems(WellKnownFolderName.SentItems, view);

            var viewError = new ItemView(1000, offset, OffsetBasePoint.Beginning)
            {
                PropertySet = PropertySet.FirstClassProperties
            };
            var findResultsError = await service.FindItems(WellKnownFolderName.Inbox, view);

            var yesterDays = DateTime.Now.AddDays(-1);
            var now = DateTime.Now.AddDays(1);
            var emailsError = findResultsError.Items.Cast<EmailMessage>().Where(x => x.DateTimeReceived > yesterDays && x.DateTimeReceived < now && x.Subject.Contains("Undeliverable:")).ToList();
            var emails = findResults.Items.Cast<EmailMessage>().Where(x => x.DateTimeReceived > yesterDays && x.DateTimeReceived < now && !emailsError.Select(e => e.DisplayTo).Contains(x.DisplayTo)).ToList();

            return emails.Select(x => x.DisplayTo).ToList();
        }
        catch (Exception ex)
        {
            return new List<string>();
        }
    }
}