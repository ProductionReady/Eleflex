using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleflex.Security.Web.Security.Roles
{
    public class RolesViewModel
    {

        public RolesViewModel()
        {
            RoleRoles = new List<RoleRoleViewModel>();
        }

        public Guid RoleKey { get; set; }
        public string RoleName { get; set; }
        public List<RoleRoleViewModel> RoleRoles { get; set; }

        
    }
}