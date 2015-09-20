using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Web.Admin.Models
{
    public class DetailsModel : Lookup
    {

        [Required]
        [MaxLength(250)]
        public override string Name { get; set; }

        [MaxLength(2000)]
        public override string Description { get; set; }
        

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