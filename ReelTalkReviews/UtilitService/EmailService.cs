using MailKit.Net.Smtp;
using MimeKit;
using ReelTalkReviews.Models;
using System.Net.Mail;

namespace ReelTalkReviews.UtilitService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration configuration) 
        {
            _config = configuration;
        }
        public void SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            var from = _config["EmailSettings:From"];
            emailMessage.From.Add(new MailboxAddress("Hi Friends ",from));
            emailMessage.To.Add(new MailboxAddress(emailModel.To,emailModel.To));
            emailMessage.Subject=emailModel.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Content)
            };
            using (var client=new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_config["EmailSettings:SmtpServer"],456,true);
                    client.Authenticate(_config["EmailSettings:From"],_config["EmailSettings:Password"]);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
