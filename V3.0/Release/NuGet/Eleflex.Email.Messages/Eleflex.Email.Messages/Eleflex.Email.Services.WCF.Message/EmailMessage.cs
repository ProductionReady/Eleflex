using System;
using System.Collections.Generic;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents a service model for a EmailProcess object.
    /// </summary>
    public partial class EmailMessage
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailMessage()
        {
            Attachments = new List<Attachment>();
        }

        /// <summary>
        /// The FromAddress.
        /// </summary>
        public virtual string FromAddress { get; set; }
        /// <summary>
        /// The ToAddress.
        /// </summary>
        public virtual string ToAddress { get; set; }
        /// <summary>
        /// The CcAddress.
        /// </summary>
        public virtual string CcAddress { get; set; }
        /// <summary>
        /// The BccAddress.
        /// </summary>
        public virtual string BccAddress { get; set; }
        /// <summary>
        /// The Subject.
        /// </summary>
        public virtual string Subject { get; set; }
        /// <summary>
        /// The Body.
        /// </summary>
        public virtual string Body { get; set; }
        /// <summary>
        /// The IsHtml.
        /// </summary>
        public virtual bool IsHtml { get; set; }
        /// <summary>
        /// Attachments.
        /// </summary>
        public virtual List<Attachment> Attachments { get; set; }
    }
}
