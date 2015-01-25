using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleflex.Security.Web.Security.Roles
{
    public class PermissionsViewModel
    {

        public PermissionsViewModel()
        {
            RolePermissions = new List<RolePermissionViewModel>();
        }

        public Guid RoleKey { get; set; }
        public string Name { get; set; }
        public List<RolePermissionViewModel> RolePermissions { get; set; }
    }
}