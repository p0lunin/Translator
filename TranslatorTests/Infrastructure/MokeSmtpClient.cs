using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Tranlator.Smtp;

namespace TranslatorTests.Infrastructure
{
    public class MokeSmtpClient : ISmtpClient
    {
        public Action<MailMessage> OnSendMessage { get; }

        public MokeSmtpClient(Action<MailMessage> onSendMessage)
        {
            OnSendMessage = onSendMessage;
        }
        public async Task SendMessage(MailMessage message)
        {
            OnSendMessage(message);
        }
    }
}