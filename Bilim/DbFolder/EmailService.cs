using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MimeKit;

namespace Bilim.DbFolder
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //var emailMessage = new MimeMessage();

            //emailMessage.From.Add(new MailboxAddress("Администрация сайта", "nurbakyt0970@gmail.com"));
            //emailMessage.To.Add(new MailboxAddress("", email));
            //emailMessage.Subject = subject;
            //emailMessage.Body = new BodyBuilder()
            //{
            //    HtmlBody = "<div style=\"color: blue;\">" + message + "</div>"
            //}.ToMessageBody();

            //using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            //{
            //    //порт кате сиякты 465
            //    await client.ConnectAsync("smtp.gmail.com", 465, true);
            //    //өз почта-паролынды жазып тексерип корш,
            //    await client.AuthenticateAsync("email", "password");
            //    await client.SendAsync(emailMessage);

            //    await client.DisconnectAsync(true);
            //}







            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("bakytzhan.shymkentbay@gmail.com", "B:USTAZ");
            // кому отправляем
            MailAddress to = new MailAddress($"{email}");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "B:USTAZ - пароль ауыстыру.";
            // текст письма
            m.Body = "<div style=\"color: blue;\">" + message + "</div>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("bakytzhan.shymkentbay@gmail.com", "12357Bahon");
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.Read();

        }
    }
}
