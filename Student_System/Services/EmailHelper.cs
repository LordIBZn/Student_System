using MailKit.Net.Smtp;
using MimeKit;


namespace Student_System.Services
{
    public class EmailHelper 
    {
        //private readonly IConfiguration _configuration;

        //public EmailHelper()
        //{
        //}

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

            MimeMessage message = new MimeMessage();
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

        //public bool SendEmail(string userEmail, string confirmationLink)
        //{
        //    var Sender = _configuration.GetValue<string>("Sender");
        //    var SenderName = _configuration.GetValue<string>("Sender");
        //    var Host = _configuration.GetValue<string>("Host");
        //    var Senderpassword = _configuration.GetValue<string>("Senderpassword");
        //    var Port = _configuration.GetValue<int>("Port");

        //    MimeMessage message = new MimeMessage();
        //    message.From.Add(new MailboxAddress(SenderName, Sender));
        //    message.To.Add(MailboxAddress.Parse(userEmail));
        //    message.Subject = "Confirmation link";

        //    message.Body = new TextPart("html")
        //    {
        //        Text = "Please confirm your account by clicking <a href=\"" + confirmationLink + "\">here</a>"
        //    };

        //    SmtpClient client = new SmtpClient();
        //    client.Connect(Host, Port, MailKit.Security.SecureSocketOptions.StartTls);
        //    client.Authenticate(Sender, Senderpassword);

        //    try
        //    {
        //        client.Send(message);
        //    }
        //    catch (Exception error)
        //    {
        //        Console.WriteLine(error);

        //    }

        //    return false;
        //}
    }
}
