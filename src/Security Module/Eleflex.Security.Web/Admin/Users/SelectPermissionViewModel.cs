using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleflex.Security.Web.Security.Users
{
    public class SelectPermissionViewModel
    {

        public SelectPermissionViewModel()
        {
            SearchPermissions = new List<Message.Permission>();
        }

        public string SearchName { get; set; }
        public List<Eleflex.Security.Message.Permission> SearchPermissions { get; set; }


    }
}