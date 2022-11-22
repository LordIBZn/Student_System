using Microsoft.AspNetCore.Identity;
using MailKit.Net.Smtp;
using MimeKit;

namespace Student_System.Models
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            MimeMessage message = new MimeKit.MimeMessage();
            message.From.Add(new MailboxAddress("Tester", "testuser34556@gmail.com"));
            message.To.Add(MailboxAddress.Parse(userEmail));
            message.Subject = "Confirmation link";

            message.Body = new TextPart("html")
            {
                Text = "Please confirm your account by clicking <a href=\"" + confirmationLink + "\">here</a>"
            };

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("testuser34556@gmail.com", "jkxwftgymcdjvwlm");
                client.Send(message);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

            return false;
        }
    }
}
