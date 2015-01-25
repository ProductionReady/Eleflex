using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleflex.Security.Web.Security.Roles
{
    public class SelectRoleViewModel
    {

        public SelectRoleViewModel()
        {
            SearchRoles = new List<Message.Role>();
        }

        public string SearchName { get; set; }
        public List<Eleflex.Security.Message.Role> SearchRoles { get; set; }


    }
}