using checkin4you.Server.Services.Interfaces;
using checkin4you.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace checkin4you.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly ILogger<EmailController> _logger;

        private readonly IMailService _mailService;

        public EmailController(
            ILogger<EmailController> logger,
            IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpPost]
        public async void Send([FromBody] MailRequest request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                _logger.LogInformation("[EmailController] Successfully sent confirmation E-Mail.", request);
            }
            catch (Exception ex)
            {
                _logger.LogError("[EmailController] Error while trying to send confirmation E-Mail", ex.Message);
            }
        }
    }
}
