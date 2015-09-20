using System.Collections.Generic;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Web.Admin.Models.Users
{
    public class SelectRoleViewModel
    {

        public SelectRoleViewModel()
        {
            SearchRoles = new List<ServiceModel.SecurityRole>();
        }

        public string SearchName { get; set; }
        public IList<ServiceModel.SecurityRole> SearchRoles { get; set; }


    }
}