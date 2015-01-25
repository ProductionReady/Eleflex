using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleflex.Security.Web.Security.Users
{
    public class PermissionsViewModel
    {

        public PermissionsViewModel()
        {
            UserPermissions = new List<UserPermissionViewModel>();
        }

        public Guid UserKey { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserPermissionViewModel> UserPermissions { get; set; }
    }
}