using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eleflex.Security.Web.Security.Roles
{
    public class DetailsViewModel
    {

        public System.Guid RoleKey { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public bool Inactive { get; set; }


        /// <summary>
        /// Inactive select items.
        /// </summary>
        public List<SelectListItem> InactiveSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "Active",Value="false"},
                    new SelectListItem(){Text = "Inactive",Value="true"},
                };
            }
        }
    }
}