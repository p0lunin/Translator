using System.Threading.Tasks;

namespace Tranlator.Services
{
    public interface IEmailingService
    {
        public Task SendMessage(string email, string message);
    }
}