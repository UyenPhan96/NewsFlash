using System.Net.Mail;

namespace Web_News.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration["EmailSettings:SMTPServer"];
            _port = int.Parse(configuration["EmailSettings:SMTPPort"]);
            _username = configuration["EmailSettings:SenderEmail"];
            _password = configuration["EmailSettings:SenderPassword"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_username),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            using (var smtpClient = new SmtpClient(_smtpServer, _port))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(_username, _password);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
