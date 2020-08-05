using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tranlator.Smtp;

namespace Tranlator.Services
{
    public class EmailingService : IEmailingService
    {
        private readonly string _host;
        private readonly ISmtpClient _smtpClient;

        public EmailingService(IOptions<Settings> options, ISmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
            _host = options.Value.Host;
        }
        public async Task SendMessage(string destination, string message)
        {
            var from = new MailAddress($"noreply@{_host}");
            var to = new MailAddress(destination);
            var mailMessage = new MailMessage(from, to) {Body = message};
            await _smtpClient.SendMessage(mailMessage);
        }
    }
}