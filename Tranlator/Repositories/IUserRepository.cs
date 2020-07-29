using System.Threading.Tasks;
using Tranlator.Models;

namespace Tranlator.Repositories
{
    public interface IUserRepository : IRepository
    {
        public Task<User> CreateUser(string name, string email);
        public Task<User> FindUserByEmail(string email);
    }
}