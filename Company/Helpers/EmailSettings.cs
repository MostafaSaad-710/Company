using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;

namespace Company.Helpers
{
    public class EmailSettings
    {
        public static bool SendEmail(Email email)
        {

            // Mail Server : Gmail
            // SMTP

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("mustafasaad2274@gmail.com", "hojpytcqdromqilp");

                client.Send("mustafasaad2274@gmail.com", email.To, email.Subject, email.Body);

                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
