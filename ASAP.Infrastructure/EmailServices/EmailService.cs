using ASAP.Infrastructure.EmailServices.Dtos;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ASAP.Infrastructure.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task NotifyClientWithUpdates(IEnumerable<string> emails, CancellationToken cancellationToken)
        {
            string subject = "notify with new data";
            string content =" new updates related to stock markets";
            var resetPasseordMessage = new EmailMessageDto(emails, subject, content);
            await SendEmail(resetPasseordMessage, cancellationToken);
        }

        public async Task SendEmail(EmailMessageDto message, CancellationToken cancellationToken)
        {
            var emailMessage = CreateEmailMessage(message);
            await Send(emailMessage, cancellationToken);
        }

        private MimeMessage CreateEmailMessage(EmailMessageDto message)
        {
            var _emailConfig = new EmailConfigurationDto();
            _emailConfig.From = _configuration["EmailConfiguration:From"];
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private async Task Send(MimeMessage mailMessage, CancellationToken cancellationToken)
        {
            var _emailConfig = new EmailConfigurationDto();
            _emailConfig.SmtpServer = _configuration["EmailConfiguration:SmtpServer"];
            _emailConfig.Port = int.Parse(_configuration["EmailConfiguration:Port"]);
            _emailConfig.Password = _configuration["EmailConfiguration:Password"];
            _emailConfig.UserName = _configuration["EmailConfiguration:UserName"];

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true , cancellationToken);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password, cancellationToken);

                    await client.SendAsync(mailMessage, cancellationToken);
                }
                catch
                {
                    // Handle exceptions
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true, cancellationToken);
                    client.Dispose();
                }
            }
        }
    }
}
