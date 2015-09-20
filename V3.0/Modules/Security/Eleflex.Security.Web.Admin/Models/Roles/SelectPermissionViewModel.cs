using System.Collections.Generic;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Web.Admin.Models.Roles
{
    public class SelectPermissionViewModel
    {

        public SelectPermissionViewModel()
        {
            SearchPermissions = new List<ServiceModel.SecurityPermission>();
        }

        public string SearchName { get; set; }
        public IList<ServiceModel.SecurityPermission> SearchPermissions { get; set; }


    }
}