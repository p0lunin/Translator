using System.Net.Mail;
using System.Threading.Tasks;

namespace Tranlator.Smtp
{
    public class SysSmtpClient : ISmtpClient
    {
        private SmtpClient _smtpClient;

        public SysSmtpClient()
        {
            _smtpClient = new SmtpClient();
        }

        public async Task SendMessage(MailMessage message)
        {
            await _smtpClient.SendMailAsync(message);
        }
    }
}