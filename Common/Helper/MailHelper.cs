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
    public class MailHelper
    {
        private const string _fromEmailPassword = "P@ss1987";
        private const string _fromEmailServer = "tantrinhlienketviet@gmail.com";
        private const string smtpHost = "smtp.gmail.com";
        private const string smtpPort = "587";
        private const string _fromEmailDisplayName = "Quản trị viên website";

        public static void SendMail(string toEmailAddress, string subject, string content)
        {

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(_fromEmailServer, _fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(_fromEmailServer, _fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }

        public static void SendMail(string toEmailAddress, string toEmailDisplayName, string fromEmailAddress, string fromEmailDisplayName, string subject, string content)
        {
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress, toEmailDisplayName));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            //var client = new SmtpClient();            
            using (var client = new SmtpClient())
            {
                client.Credentials = new NetworkCredential(_fromEmailServer, _fromEmailPassword);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toEmailAddress"></param>
        /// <param name="toEmailDisplayName"></param>
        /// <param name="fromEmailAddress"></param>
        /// <param name="fromEmailDisplayName"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <returns></returns>
        public static async Task SendMailAsync(string toEmailAddress, string toEmailDisplayName, string fromEmailAddress, string fromEmailDisplayName, string subject, string content, string[] cc, string[] bcc)
        {
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress, toEmailDisplayName));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.IsBodyHtml = true;

            if (bcc != null && bcc.Count() > 0)
            {
                Array.ForEach(bcc, m => message.Bcc.Add(new MailAddress(m)));
            }
            if (cc != null && cc.Count() > 0)
            {
                Array.ForEach(cc, m => message.CC.Add(new MailAddress(m)));
            }
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(_fromEmailServer, _fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = true;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            await client.SendMailAsync(message);
        }
    }
}
