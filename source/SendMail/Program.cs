using BusinessEntities;
using BusinessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SendMail
{
    class Program
    {
        static void Main(string[] args)
        {
            MailManager mailManager = new MailManager();
            ICollection<Mail> mailList =  mailManager.GetAllNotSendEmails();
            foreach(var mail in mailList)
            {
                try
                {
                    Core.Utility.SendEmail(mail.Sender, mail.Receiver,
                                       mail.MailCc, mail.MailBcc,mail.Subject,
                                       mail.MailBody, mail.IsHtml);
                    mailManager.UpdateMailAsSend(mail.MailId);
                }
                catch
                {

                }
            }

            try
            {
                var WebReq = (HttpWebRequest)WebRequest.Create(string.Format("http://www.pizzaness.com/Login.aspx"));
                WebReq.Method = "GET";
                WebReq.GetResponse();
            }
            catch { }
        }
    }
}
