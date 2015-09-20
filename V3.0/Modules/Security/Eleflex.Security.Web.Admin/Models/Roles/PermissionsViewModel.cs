using System;
using System.Collections.Generic;

namespace Eleflex.Security.Web.Admin.Models.Roles
{
    public class PermissionsViewModel
    {

        public PermissionsViewModel()
        {
            SecurityRolePermissions = new List<RolePermissionViewModel>();
        }

        public Guid SecurityRoleKey { get; set; }
        public string Name { get; set; }
        public List<RolePermissionViewModel> SecurityRolePermissions { get; set; }
    }
}