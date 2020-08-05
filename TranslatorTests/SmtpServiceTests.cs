using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Tranlator;
using Tranlator.Services;
using TranslatorTests.Infrastructure;

namespace TranslatorTests
{
    public class SmtpServiceTests
    {
        [Test]
        public async Task TestSendMessage()
        {
            const string content = "content";
            const string mail = "mail@mail.com";
            var options = new Settings("host");
            var mokeClient = new MokeSmtpClient(message =>
            {
                Assert.AreEqual(content, message.Body);
                Assert.AreEqual(new MailAddress("noreply@host"), message.From);
            });
            var service = new EmailingService(options, mokeClient);

            await service.SendMessage(mail, content);
        }
    }
}