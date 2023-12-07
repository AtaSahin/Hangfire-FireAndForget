using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Hangfire.Web.Services
{
    public class EmailSender : IEmailSender
    {
       
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration) { 
        
        _configuration=configuration;
        }
        public async Task Sender(string userId, string message)
        {
   
            var apiKey = _configuration.GetSection("APIs")["SendGridApi"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("atas01649@gmail.com", "Example User");
            var subject = "www.mysite.com bilgilendirme";
            var to = new EmailAddress("adilatasahin@hotmail.com", "Example User");
          //  var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
         var response = await client.SendEmailAsync(msg);

        }
    }
}
