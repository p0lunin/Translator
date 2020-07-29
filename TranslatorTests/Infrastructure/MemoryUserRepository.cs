using System.Collections.Generic;
using System.Threading.Tasks;
using Tranlator.Exceptions;
using Tranlator.Models;
using Tranlator.Repositories;

namespace TranslatorTests.Infrastructure
{
    class MemoryUserRepository : IUserRepository
    {
        public List<User> _users;

        public MemoryUserRepository()
        {
            _users = new List<User>();
        }
        public async Task SaveChanges()
        {
            
        }

        public async Task<User> CreateUser(string name, string email)
        {
            var user = new User
            {
                Name = name,
                Email = email,
                Projects = new List<Project>()
            };
            _users.Add(user);
            return user;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = _users.Find(user => user.Email.Equals(email));
            return user switch
            {
                null => throw new RecordNotFoundException("user"),
                _ => user
            };
        }
    }
}