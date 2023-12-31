using System.Net;
using System.Net.Mail;

namespace Ecommerce.DAL
{
    public class EmailManagement
    {
        public static void SendEmail(Email email)
        {
            //SMTP => Simple Mail Transfer Protocol
            var client = new SmtpClient("smtp.gmail.com", 587);// accept two parameters (host, port) => host name of the server that the email will be sent from it like "mti.com"
            client.EnableSsl = true; // the email will be sent encrypted
            client.Credentials = new NetworkCredential("laethraed0@gmail.com","21510568"); // accept two parameters (username, password) => username and password of the email that will be sent from it
            client.Send("laethraed0@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
