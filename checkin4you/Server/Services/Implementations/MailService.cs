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
        private readonly ILogger<MailService> _logger;

        private readonly MailSettings _mailSettings;

        public MailService(
            ILogger<MailService> logger,
            IOptions<MailSettings> mailSettings)
        {
            _logger = logger;
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
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

                _logger.LogInformation("[MailService] Successfully sent confirmation E-Mail.", mailRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError("[MailService] Error while trying to send confirmation E-Mail", ex.Message);
            }
        }
    }
}
