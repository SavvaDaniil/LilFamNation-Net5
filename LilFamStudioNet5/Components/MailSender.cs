using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Components
{
    public class MailSender
    {

        private const string emailDomain = "info@stgonline-net.ru";
        private const string emailDomainPassword = "jDPewlk3@54";

        public void sendMailToUser(string username, string title, string messageHTML)
        {
            Thread tMailDeveloper = new Thread(() => threadSend(username, title, messageHTML));
            tMailDeveloper.Start();
        }


        private void threadSend(
            string mail_to,
            string subject,
            string messageHtml
        )
        {
            System.Diagnostics.Debug.WriteLine("threadSend to: " + mail_to);
            try
            {
                MailAddress from = new MailAddress(emailDomain, "DoNotReply");
                MailAddress to = new MailAddress(mail_to);
                MailMessage m = new MailMessage(from, to);
                m.Subject = subject;
                m.Body = messageHtml;
                m.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("wpl42.hosting.reg.ru", 587);

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailDomain, emailDomainPassword);
                smtp.EnableSsl = true;

                smtp.Send(m);
                smtp.Dispose();

                System.Diagnostics.Debug.WriteLine("Сообщение отправлено");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception caught in threadSend(): {0}", ex.ToString());
            }
        }
    }
}
