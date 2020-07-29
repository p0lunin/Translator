using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Tranlator.Services
{
    public class SmtpService : ISmtpService
    {
        private readonly string _host;
        private readonly SmtpClient _smtpClient;

        public SmtpService(IOptions<Settings> options)
        {
            _smtpClient = new SmtpClient();
            _host = options.Value.Host;
        }
        public async Task SendMessage(string destination, string message)
        {
            var from = new MailAddress($"noreply@{_host}");
            var to = new MailAddress(destination);
            var mailMessage = new MailMessage(from, to) {Body = message};
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}