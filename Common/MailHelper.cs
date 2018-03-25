using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace Common
{
    public static class MailHelper
    {
        public static void SendMail(string toEmailAddress, string subject, string content)
        {            
            var fromEmailServer = ConfigurationManager.AppSettings["FromEmailServer"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailServer, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailServer, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }

        public static void SendMail(string toEmailAddress, string toEmailDisplayName, string fromEmailAddress, string fromEmailDisplayName, string subject, string content)
        {   
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var fromEmailServer = ConfigurationManager.AppSettings["FromEmailServer"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
                        
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress, toEmailDisplayName));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            //var client = new SmtpClient();            
            using (var client = new SmtpClient())
            {
                client.Credentials = new NetworkCredential(fromEmailServer, fromEmailPassword);
                client.Host = smtpHost;
                client.EnableSsl = true;
                client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;                
                client.Send(message);
                //await client.SendMailAsync(email.Message);
                //return true;
            }

        }

        public static void SendMails(string[] emails, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            foreach (var mail in emails)
            {
                string body = content;
                MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(mail));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                var client = new SmtpClient();
                client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                client.Host = smtpHost;
                client.EnableSsl = enabledSsl;
                client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                client.Send(message);
            }
            

        }


    }
}
