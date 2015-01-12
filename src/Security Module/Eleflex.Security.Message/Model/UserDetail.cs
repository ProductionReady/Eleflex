using System;
using System.Collections.Generic;

namespace Eleflex.Security.Message
{
    public partial class UserDetail : User
    {
        /// <summary>
        /// Assigned Roles (and linked roles).
        /// </summary>
        public List<Role> Roles { get; set; }
        /// <summary>
        /// Assigned User Permissions
        /// </summary>
        public List<Permission> UserPermissions { get; set; }

        /// <summary>
        /// Assigned User Claims
        /// </summary>
        public List<Permission> UserClaims { get; set; }
    }
}
