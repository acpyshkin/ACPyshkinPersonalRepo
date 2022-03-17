namespace BuisnessLogic
{
    using Domain;
    using Microsoft.Extensions.Logging;

    public class SmsSenderService : ISmsSenderService
    {
        private readonly ILogger<SmsSenderService> _logger;

        public SmsSenderService(ILogger<SmsSenderService> logger)
        {
            _logger = logger;
        }

        public void SendSms(int phonenumber, string text)
        {
            _logger.LogDebug($"SMS message has sent to number {phonenumber} with text {text}");
        }
    }
}