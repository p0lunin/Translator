using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tranlator.Exceptions;
using Tranlator.Models;

namespace Tranlator.Repositories
{
    public class EfUserRepository : IUserRepository
    {
        private TranslatorContext _ctx;

        public EfUserRepository(TranslatorContext ctx)
        {
            _ctx = ctx;
        }
        
        public async Task<User> CreateUser(string name, string email)
        {
            var user = new User
            {
                Name = name,
                Email = email,
            };
            await _ctx.Users.AddAsync(user);
            return user;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            try
            {
                return await _ctx.Users.Where(user => user.Email.Equals(email)).FirstAsync();
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("user");
            }
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}