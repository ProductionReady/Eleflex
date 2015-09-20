using System;
using System.Collections.Generic;

namespace Eleflex.Security.Web.Admin.Models.Roles
{
    public class RolesViewModel
    {

        public RolesViewModel()
        {
            RoleRoles = new List<RoleRoleViewModel>();
        }

        public Guid SecurityRoleKey { get; set; }
        public string RoleName { get; set; }
        public List<RoleRoleViewModel> RoleRoles { get; set; }

        
    }
}