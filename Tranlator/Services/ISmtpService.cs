using System.Threading.Tasks;

namespace Tranlator.Services
{
    public interface ISmtpService
    {
        public Task SendMessage(string email, string message);
    }
}