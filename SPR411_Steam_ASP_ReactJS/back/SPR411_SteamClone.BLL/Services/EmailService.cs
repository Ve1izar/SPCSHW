using System.Net;
using System.Net.Mail;

namespace SPR411_SteamClone.BLL.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("v1adg0re4k0@gmail.com", "Steam Clone Support");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "*********"; 

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true // Дозволяє відправляти красиві HTML-листи
            };

            await smtp.SendMailAsync(message);
        }
    }
}