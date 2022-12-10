using MailKit.Net.Smtp;
using MimeKit;

namespace Student_System.Models
{
    public class EmailHelper
    {
        //private readonly IConfiguration _configuration;

        //public EmailHelper(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public bool SendEmail(string userEmail, string confirmationLink)
        {
            //var sender = _configuration.GetValue<string>("Sender");
            //var smtpHost = _configuration.GetValue<string>("smtpHost");
            //var senderpassword = _configuration.GetValue<string>("senderpassword");
            //var port = _configuration.GetValue<int>("port");

            MimeMessage message = new MimeKit.MimeMessage();
            message.From.Add(new MailboxAddress("Tester", "testuser34556@gmail.com"));
            message.To.Add(MailboxAddress.Parse(userEmail));
            message.Subject = "Confirmation link";

            message.Body = new TextPart("html")
            {
                Text = "Please confirm your account by clicking <a href=\"" + confirmationLink + "\">here</a>"
            };

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("testuser34556@gmail.com", "jkxwftgymcdjvwlm");

            try
            {
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
