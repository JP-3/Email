using System.Net;
using System.Net.Mail;

namespace Emails
{
    public class Email
    {
        public void SendEmail(string subject, string body)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(@"C:\\Plex\key.txt"))
            {
                data.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            }

            Console.WriteLine(data[PropertiesEnum.gName.ToString()]);

            // Create a MailMessage object
            MailMessage mailMessage = new MailMessage(data[PropertiesEnum.gName.ToString()], data[PropertiesEnum.gName.ToString()]);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            // Create a SmtpClient object
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            smtpClient.Port = 587; // Gmail SMTP port
            smtpClient.Credentials = new NetworkCredential(data[PropertiesEnum.gName.ToString()], data[PropertiesEnum.gKey.ToString()]);
            smtpClient.EnableSsl = true; // Enable SSL/TLS

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
