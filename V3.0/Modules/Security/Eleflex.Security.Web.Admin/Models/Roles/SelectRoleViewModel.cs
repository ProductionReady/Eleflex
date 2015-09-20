using System.Collections.Generic;
using ServiceModel = Eleflex;
using Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Security.Web.Admin.Models.Roles
{
    public class SelectRoleViewModel
    {

        public SelectRoleViewModel()
        {
            SearchRoles = new List<IdentityRole>();
        }

        public string SearchName { get; set; }
        public IList<IdentityRole> SearchRoles { get; set; }


    }
}