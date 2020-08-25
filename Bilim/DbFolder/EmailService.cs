using System;
using System.Threading.Tasks;
using MimeKit;

namespace Bilim.DbFolder
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "nurbakyt0970@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new BodyBuilder()
            {
                HtmlBody = "<div style=\"color: blue;\">" + message + "</div>"
            }.ToMessageBody();

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                //порт кате сиякты 465
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                //өз почта-паролынды жазып тексерип корш,
                await client.AuthenticateAsync("email", "password");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
