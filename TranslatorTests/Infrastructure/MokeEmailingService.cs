using System;
using System.Threading.Tasks;
using Tranlator.Services;

namespace TranslatorTests.Infrastructure
{
    class MokeEmailingService : IEmailingService
    {
        public Action<string, string> OnSendMessage { get; }

        public MokeEmailingService(Action<string, string> onSendMessage)
        {
            OnSendMessage = onSendMessage;
        }
        public async Task SendMessage(string email, string message)
        {
            OnSendMessage(email, message);
        }
    }
}