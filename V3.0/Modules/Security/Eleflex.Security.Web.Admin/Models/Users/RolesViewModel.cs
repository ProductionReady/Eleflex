using System;
using System.Collections.Generic;

namespace Eleflex.Security.Web.Admin.Models.Users
{
    public class RolesViewModel
    {

        public RolesViewModel()
        {
            UserRoles = new List<UserRoleViewModel>();
        }

        public Guid SecurityUserKey { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserRoleViewModel> UserRoles { get; set; }

        
    }
}