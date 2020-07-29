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