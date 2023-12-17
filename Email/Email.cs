using System.Net;
using System.Net.Mail;

namespace MyEmails
{
    public class Email
    {
        public void SendEmail(string subject) => SendEmail(subject, string.Empty);

        public void SendEmail(string subject, string body) => SendEmail(subject, body, string.Empty);

        public void SendEmail(string subject, string body, string attachmentPath)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(@"C:\\git\key.txt"))
            {
                data.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            }

            // Create a MailMessage object
            MailMessage mailMessage = new MailMessage(data[PropertiesEnum.gName.ToString()], data[PropertiesEnum.gName.ToString()]);
            using (mailMessage)
            {
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                if (attachmentPath != string.Empty)
                {
                    mailMessage.Attachments.Add(new Attachment(attachmentPath));
                }

                // Create a SmtpClient object
                SmtpClient smtpClient = new SmtpClient(data[PropertiesEnum.SmtpClient.ToString()]);

                using (new SmtpClient(data[PropertiesEnum.SmtpClient.ToString()]))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(data[PropertiesEnum.gName.ToString()], data[PropertiesEnum.gKey.ToString()]);
                    smtpClient.EnableSsl = true; // Enable SSL/TLS
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
