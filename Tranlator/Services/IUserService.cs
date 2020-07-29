using System.Threading.Tasks;
using Tranlator.ViewModels;

namespace Tranlator.Services
{
    public interface IUserService
    {
        public Task SendAuthLink(string email);
        public Task<UserAuthorizedResult> AuthUser(string link);
    }
}