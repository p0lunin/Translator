using System;
using System.Threading.Tasks;
using Tranlator.Models;

namespace Tranlator.Repositories
{
    public interface IAuthLinksRepository : IRepository
    {
        public Task<AuthLink> FindLink(string link);
        public Task<AuthLink> CreateLink(string email, string link, DateTime expires);
    }
}