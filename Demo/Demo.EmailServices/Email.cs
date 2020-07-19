using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace Demo.EmailServices
{
    public class Email
    {
        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="to">Message to address</param>
        /// <param name="body">Text of message to send</param>
        /// <param name="subject">Subject line of message</param>
        /// <param name="fromAddress">Message from address</param>
        /// <param name="fromDisplay">Display name for "message from address"</param>
        /// <param name="credentialUser">User whose credentials are used for message send</param>
        /// <param name="credentialPassword">User password used for message send</param>
        /// <param name="attachments">Optional attachments for message</param>
        public static void SendEmail(string to,
            string body,
            string subject,
            string fromAddress,
            string fromDisplay,
            string credentialUser = "",
            string credentialPassword = "",
            params MailAttachment[] attachments)
        {
            var host = ConfigurationManager.AppSettings["SMTPHost"];
            var port = ConfigurationManager.AppSettings["SMTPPort"];
            var userName = ConfigurationManager.AppSettings["SMTPUserName"];
            var password = ConfigurationManager.AppSettings["SMTPPassword"];
            if (credentialUser == string.Empty)
            {
                credentialUser = userName;
            }
            if (credentialPassword == string.Empty)
            {
                credentialPassword = password;
            }

            try
            {
                var mail = new MailMessage { Body = body, IsBodyHtml = true };
                mail.To.Add(new MailAddress(to));
                mail.From = new MailAddress(fromAddress, fromDisplay, Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                foreach (var ma in attachments)
                {
                    mail.Attachments.Add(ma.File);
                }
                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = Convert.ToInt32(port),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword)
                };
                smtp.Send(mail);
            }
            catch (Exception)
            {
                var sb = new StringBuilder(1024);
                sb.Append("\nTo:" + to);
                sb.Append("\nbody:" + body);
                sb.Append("\nsubject:" + subject);
                sb.Append("\nfromAddress:" + fromAddress);
                sb.Append("\nfromDisplay:" + fromDisplay);
                sb.Append("\ncredentialUser:" + credentialUser);
                sb.Append("\ncredentialPasswordto:" + credentialPassword);
                sb.Append("\nHosting:" + host);
            }
        }
    }
}
