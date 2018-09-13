using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    [Serializable]
    public class Mail
    {
        public Mail()
        {

        }

        public virtual long MailId { get; set; }

        public virtual string Subject { get; set; }

        public virtual string Sender { get; set; }

        public virtual string Receiver { get; set; }

        public virtual string MailCc { get; set; }

        public virtual string MailBcc { get; set; }

        public virtual string MailBody { get; set; }

        public virtual bool IsHtml { get; set; }

        public virtual bool IsSend { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual Int64? ReferenceId { get; set; }

        public virtual string ReferenceType { get; set; }
    }
}
