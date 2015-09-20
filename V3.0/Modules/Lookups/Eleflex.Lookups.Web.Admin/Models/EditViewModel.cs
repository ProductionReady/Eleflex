using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eleflex.Lookups.Web.Admin.Models
{
    public class EditViewModel
    {

        public EditViewModel()
        {
            Active = true;
        }

        [Required]
        public System.Guid? LookupKey { get; set; }
        public System.Guid? ParentLookupKey { get; set; }
        public System.DateTimeOffset CreateDate { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public string ExtraData { get; set; }
        public bool Active { get; set; }        
        public int? SortOrder { get; set; }
        
        public string SuccessMessage { get; set; }


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