using System.Collections.Generic;
using System.Web.Mvc;
using ServiceModel = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Web.Admin.Models
{
    /// <summary>
    /// Email list model.
    /// </summary>
    public class ListModel
    {
        
        /// <summary>
        /// The name.
        /// </summary>
        public virtual string CreateDate { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        public virtual string SendDate { get; set; }

        /// <summary>
        /// THe subject
        /// </summary>
        public virtual string Subject { get; set; }

        /// <summary>
        /// To.
        /// </summary>
        public virtual string FromAddress { get; set; }
        /// <summary>
        /// To.
        /// </summary>
        public virtual string ToAddress { get; set; }
        /// <summary>
        /// To.
        /// </summary>
        public virtual string BccAddress { get; set; }
        /// <summary>
        /// To.
        /// </summary>
        public virtual string CcAddress { get; set; }

        /// <summary>
        /// Body
        /// </summary>
        public virtual string Body { get; set; }


        /// <summary>
        /// Max records to return.
        /// </summary>
        public int? MaxRecords { get; set; }

        /// <summary>
        /// Search error
        /// </summary>
        public bool? IsError { get; set; }

        /// <summary>
        /// Error select items.
        /// </summary>
        public List<SelectListItem> ErrorSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "",Value=""},
                    new SelectListItem(){Text = "True",Value="true"},
                    new SelectListItem(){Text = "False",Value="false"},
                };
            }
        }

        /// <summary>
        /// Items.
        /// </summary>
        public IList<ServiceModel.EmailProcess> Items { get; set; }
    }
}