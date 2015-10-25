using System;

namespace Eleflex.Email.Services.WCF.Message
{
    public partial class SendEmailRequest : RequestItem<EmailMessage>
    {

        public SendEmailRequest() : base() { }

        public SendEmailRequest(EmailMessage item) : base(item) { }

        public virtual DateTimeOffset? FutureSendDate { get; set; }

    }
}
