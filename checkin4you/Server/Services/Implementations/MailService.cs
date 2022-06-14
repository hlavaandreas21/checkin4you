using checkin4you.Server.Services.Interfaces;
using checkin4you.Server.Settings;
using checkin4you.Shared.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace checkin4you.Server.Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            BodyBuilder builder = new();
            builder.TextBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using SmtpClient smtp = new();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, true);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
