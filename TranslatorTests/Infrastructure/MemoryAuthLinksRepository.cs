using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tranlator.Exceptions;
using Tranlator.Models;
using Tranlator.Repositories;

namespace TranslatorTests.Infrastructure
{
    class MemoryAuthLinksRepository : IAuthLinksRepository
    {
        public List<AuthLink> _links;

        public MemoryAuthLinksRepository()
        {
            _links = new List<AuthLink>();
        }
        public async Task SaveChanges()
        {
            
        }
        
        public async Task<AuthLink> FindLink(string link)
        {
            var authLink = _links.Find(l => l.Link.Equals(link));
            return authLink switch
            {
                null => throw new RecordNotFoundException("link"),
                _ => authLink
            };
        }

        public async Task<AuthLink> CreateLink(string email, string link, DateTime expires)
        {
            var authLink = new AuthLink
            {
                Email = email,
                Link = link,
                Expires = expires
            };
            _links.Add(authLink);
            return authLink;
        }
    }
}