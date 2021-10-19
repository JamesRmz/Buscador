using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace DataAccess.MailServices
{
    public abstract class MasterMailServer
    {
        private SmtpClient smtClient;
        protected string senderMail { get; set; }
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }

        protected void initializeSmtpClient()
        {
            smtClient = new SmtpClient();
            smtClient.Credentials = new NetworkCredential(senderMail, password);
            smtClient.Host = host;
            smtClient.Port = port;
            smtClient.EnableSsl = ssl;
        }
        public void sendMail(string subject,string body,List<string> recipientMail)
        {
            var mailMessage = new MailMessage();
            try
            {
                mailMessage.From = new MailAddress(senderMail);
                foreach(string mail in recipientMail)
                {
                    mailMessage.To.Add(mail);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                smtClient.Send(mailMessage);

            }
            catch (Exception ex )
            {

            }
            finally
            {
                mailMessage.Dispose();
                smtClient.Dispose();
            }
        }


    }
}
