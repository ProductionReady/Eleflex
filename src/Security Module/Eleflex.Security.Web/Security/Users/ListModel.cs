using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleflex.Security.Message;

namespace Eleflex.Security.Web.Security.Users
{
    public class ListModel
    {

        public string Email { get; set; }

        public string Username { get; set; }

        public string Inactive { get; set; }

        public int? MaxRecords { get; set; }

        public List<SelectListItem> InactiveSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "",Value=""},
                    new SelectListItem(){Text = "Active",Value="false"},
                    new SelectListItem(){Text = "Inactive",Value="true"},
                };
            }
        }

        public List<User> Items { get; set; }
    }
}