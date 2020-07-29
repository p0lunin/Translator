using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Tranlator;
using Tranlator.Models;
using Tranlator.Services;
using TranslatorTests.Infrastructure;

namespace TranslatorTests
{
    public class UserServiceTests
    {
        private MemoryUserRepository _userRepository;
        private MemoryAuthLinksRepository _authLinksRepository;
        private OptionsWrapper<Settings> _options;
        private OneValueRandomGeneratorService<string> _randomGeneratorService;
        [SetUp]
        public void Setup()
        {
            _userRepository = new MemoryUserRepository();
            _authLinksRepository = new MemoryAuthLinksRepository();
            _randomGeneratorService = new OneValueRandomGeneratorService<string>("123");
            _options = new OptionsWrapper<Settings>(new Settings("host"));
        }

        [Test]
        public void TestSendAuthLink()
        {
            Task.Run(async () =>
            {
                var userService = new UserService(
                    _randomGeneratorService, 
                    _userRepository,
                    _authLinksRepository,
                    new MokeSmtpService((email, message) =>
                    {
                        Assert.AreEqual(email, "fake@mail.com");
                        Assert.AreEqual(message, "host/users/auth?key=123");
                    }),
                    _options
                    );
                await userService.SendAuthLink("fake@mail.com");
                var expected = new AuthLink
                {
                    Email = "fake@mail.com",
                    Link = "123",
                };
                var actual = _authLinksRepository._links.First();
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.Link, actual.Link);
            });
        }
    }
}