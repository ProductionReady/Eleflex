using System;
using System.Collections.Generic;

namespace Eleflex.Security.Web.Admin.Models.Users
{
    public class PermissionsViewModel
    {

        public PermissionsViewModel()
        {
            UserPermissions = new List<UserPermissionViewModel>();
        }

        public Guid SecurityUserKey { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserPermissionViewModel> UserPermissions { get; set; }
    }
}