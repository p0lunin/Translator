using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Tranlator;
using Tranlator.Exceptions;
using Tranlator.Models;
using Tranlator.Repositories;
using Tranlator.Services;

namespace TranslatorTests
{
    class MokeSmtpService : ISmtpService
    {
        public Action<string, string> OnSendMessage { get; }

        public MokeSmtpService(Action<string, string> onSendMessage)
        {
            OnSendMessage = onSendMessage;
        }
        public async Task SendMessage(string email, string message)
        {
            OnSendMessage(email, message);
        }
    }

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

    class OneValueRandomGeneratorService<T> : IRandomGeneratorService<T>
    {
        private T _value;

        public OneValueRandomGeneratorService(T value)
        {
            _value = value;
        }
        public T Generate()
        {
            return _value;
        }
    }
    
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSendAuthLink()
        {
            Task.Run(async () =>
            {
                var userRepository = new MemoryUserRepository();
                var authLinksRepository = new MemoryAuthLinksRepository();
                
                var userService = new UserService(
                    new OneValueRandomGeneratorService<string>("123"), 
                    userRepository,
                    authLinksRepository,
                    new MokeSmtpService((email, message) =>
                    {
                        Assert.AreEqual(email, "fake@mail.com");
                        Assert.AreEqual(message, "host/users/auth?key=123");
                    }),
                    new OptionsWrapper<Settings>(new Settings("host"))
                    );
                await userService.SendAuthLink("fake@mail.com");
                var expected = new AuthLink
                {
                    Email = "fake@mail.com",
                    Link = "123",
                };
                var actual = authLinksRepository._links.First();
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.Link, actual.Link);
            });
        }
    }
}