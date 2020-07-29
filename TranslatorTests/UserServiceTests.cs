using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Tranlator;
using Tranlator.Models;
using Tranlator.Services;
using Tranlator.ViewModels;
using TranslatorTests.Infrastructure;

namespace TranslatorTests
{
    public class UserServiceTests
    {
        private MemoryUserRepository _userRepository;
        private MemoryAuthLinksRepository _authLinksRepository;
        private OptionsWrapper<Settings> _options;
        private OneValueRandomGeneratorService<string> _randomGeneratorService;
        private const string FakeMail = "fake@gmail.com";
        private const string RandomValue = "123";
        [SetUp]
        public void Setup()
        {
            _userRepository = new MemoryUserRepository();
            _authLinksRepository = new MemoryAuthLinksRepository();
            _randomGeneratorService = new OneValueRandomGeneratorService<string>(RandomValue);
            _options = new OptionsWrapper<Settings>(new Settings("host"));
        }

        [Test]
        public async Task TestSendAuthLink()
        {
            var userService = new UserService(
                _randomGeneratorService, 
                _userRepository,
                _authLinksRepository,
                new MokeSmtpService((email, message) =>
                {
                    Assert.AreEqual(email, FakeMail);
                    Assert.AreEqual(message, $"host/users/auth?key={RandomValue}");
                }),
                _options
                );
            await userService.SendAuthLink(FakeMail);
            var expected = new AuthLink
            {
                Email = FakeMail,
                Link = RandomValue,
            };
            var actual = _authLinksRepository._links.First();
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Link, actual.Link);
        }
        
        [Test]
        public async Task TestAuthNewUser()
        {
            var userService = new UserService(
                _randomGeneratorService, 
                _userRepository,
                _authLinksRepository,
                new MokeSmtpService((email, message) =>
                {
                    
                }),
                _options
            );
            await userService.SendAuthLink(FakeMail);
            var actual = await userService.AuthUser(RandomValue);
            var expected = new UserAuthorizedResult(FakeMail, true);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public async Task TestAuthOldUser()
        {
            const string Username = "Dordoa";
            await _userRepository.CreateUser(Username, FakeMail);
            var userService = new UserService(
                _randomGeneratorService, 
                _userRepository,
                _authLinksRepository,
                new MokeSmtpService((email, message) =>
                {
                    
                }),
                _options
            );
            await userService.SendAuthLink(FakeMail);
            var actual = await userService.AuthUser(RandomValue);
            var expected = new UserAuthorizedResult(Username);
            Assert.AreEqual(expected, actual);
        }
    }
}