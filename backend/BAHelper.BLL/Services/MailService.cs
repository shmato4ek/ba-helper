using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.DAL.Context;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BAHelper.BLL.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMail(string toEmail, string subject, string message, string name = "")
        {
            string apiKey = _configuration["SendGridKey"];
            string fromEmail = _configuration["SendGridFromEmail"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, "Admin BAHelper");
            var to = new EmailAddress(toEmail, toEmail);
            if (name != "")
            {
                message = $"Dear {name}, {message}";
            }
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            await client.SendEmailAsync(msg);
        }
    }
}
