using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eleflex.Email.Web.Admin.Models
{
    public class EditViewModel
    {

        public EditViewModel()
        {
        }


        public virtual long? EmailProcessKey { get; set; }

        /// <summary>
        /// The FromAddress.
        /// </summary>
        [Required]
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
        /// The Future send date
        /// </summary>
        public virtual DateTimeOffset? FutureSendDate { get; set; }


        public string SuccessMessage { get; set; }


    }
}