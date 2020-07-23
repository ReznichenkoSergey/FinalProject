using FinalProject.Infrastructure.Configuration;
using FinalProject.Infrastructure.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FinalProject.Infrastructure.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IOptions<EmailConfig> _config;

        public MessageService(IOptions<EmailConfig> config)
        {
            _config = config;
        }

        public void SendMessage(string toAddress)
        {
            MimeMessage message = new MimeMessage();
            var from = new MailboxAddress(_config.Value.SenderName, _config.Value.SenderEmail);
            message.From.Add(from);
            //
            var to = new MailboxAddress(toAddress.Substring(0, toAddress.IndexOfAny(new char[] { '@' })), toAddress);
            message.To.Add(to);
            message.Subject = _config.Value.Subject;
            //
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = _config.Value.TextContent;
            bodyBuilder.HtmlBody = _config.Value.HtmlContent;
            message.Body = bodyBuilder.ToMessageBody();
            //
            using SmtpClient client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, ce, e) => true;
            client.Connect(_config.Value.SmtpServer, _config.Value.Port, true);
            client.Authenticate(_config.Value.SenderEmail, _config.Value.Password);
            //
            client.Send(message);
            client.Disconnect(true);
        }

    }
}
