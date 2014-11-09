using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security
{
    /// <summary>
    /// User with detailed information, including user profile, roles and permissions.
    /// </summary>
    public partial class UserDetail : User
    {        
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserDetail()
        {
            Roles = new List<Role>();
            UserPermissions = new List<Permission>();
        }
        /// <summary>
        /// Assigned Roles (and linked roles).
        /// </summary>
        public List<Role> Roles { get; protected set; }
        /// <summary>
        /// Assigned User Permissions (does not include role permissions)
        /// </summary>
        public List<Permission> UserPermissions { get; protected set; }
    }
}
