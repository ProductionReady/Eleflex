using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleflex.Security.Web.Security.Users
{
    public class RolesViewModel
    {

        public RolesViewModel()
        {
            UserRoles = new List<UserRoleViewModel>();
        }

        public Guid UserKey { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserRoleViewModel> UserRoles { get; set; }

        
    }
}