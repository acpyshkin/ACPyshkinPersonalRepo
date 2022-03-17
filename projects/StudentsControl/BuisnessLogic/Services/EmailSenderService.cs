namespace BuisnessLogic
{
    using Domain;
    using Microsoft.Extensions.Logging;

    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(ILogger<EmailSenderService> logger)
        {
            _logger = logger;
        }

        public void SendEmail(string email, string subject, string text)
        {
            _logger.LogDebug($"Email has send to {email} adress with subject: \"{subject}\" and message: \"{text}\"");
        }
    }
}