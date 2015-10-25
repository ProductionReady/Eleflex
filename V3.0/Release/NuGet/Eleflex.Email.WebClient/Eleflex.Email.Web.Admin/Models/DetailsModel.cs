using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Web.Admin.Models
{
    public class DetailsModel : EmailMessage
    {

        [Required]
        public override string FromAddress { get; set; }
        

        /// <summary>
        /// Inactive select items.
        /// </summary>
        public List<SelectListItem> ActiveSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "Active",Value="true"},
                    new SelectListItem(){Text = "Inactive",Value="false"},
                };
            }
        }

    }
}