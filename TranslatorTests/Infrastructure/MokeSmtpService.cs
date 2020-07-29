using System;
using System.Threading.Tasks;
using Tranlator.Services;

namespace TranslatorTests.Infrastructure
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
}