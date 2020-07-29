using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tranlator.Exceptions;
using Tranlator.Models;

namespace Tranlator.Repositories
{
    public class EfAuthLinksRepository : IAuthLinksRepository
    {
        private TranslatorContext _ctx;

        public EfAuthLinksRepository(TranslatorContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<AuthLink> FindLink(string link)
        {
            try
            {
                return await _ctx.LinksToAuth.Where(l => l.Link.Equals(link)).FirstAsync();
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("link");
            }
        }

        public async Task<AuthLink> CreateLink(string email, string link, DateTime expires)
        {
            var authLink = new AuthLink
            {
                Email = email,
                Link = link,
                Expires = expires,
            };
            await _ctx.LinksToAuth.AddAsync(authLink);
            return authLink;
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}