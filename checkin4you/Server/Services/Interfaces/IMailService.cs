using checkin4you.Shared.Models;

namespace checkin4you.Server.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
