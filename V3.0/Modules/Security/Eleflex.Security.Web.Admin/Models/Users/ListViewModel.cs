using System.Collections.Generic;
using System.Web.Mvc;
using Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Security.Web.Admin.Models.Users
{
    /// <summary>
    /// User list model.
    /// </summary>
    public class ListViewModel
    {

        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Active { get; set; }

        public int? MaxRecords { get; set; }

        public List<SelectListItem> ActiveSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "",Value=""},
                    new SelectListItem(){Text = "Active",Value="true"},
                    new SelectListItem(){Text = "Inactive",Value="false"},
                };
            }
        }

        public IList<IdentityUser> Items { get; set; }
    }
}