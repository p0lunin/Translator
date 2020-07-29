using System.Net.Mail;
using System.Threading.Tasks;

namespace Tranlator.Smtp
{
    public interface ISmtpClient
    {
        public Task SendMessage(MailMessage message);
    }
}