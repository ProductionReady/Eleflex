using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security.Model
{
    /// <summary>
    /// Defines a role with extended information and relationships.
    /// </summary>
    public partial class RoleDetail : Role
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RoleDetail()
        {
            Permissions = new List<Permission>();
        }

        /// <summary>
        /// Permissions.
        /// </summary>
        public List<Permission> Permissions { get; set; }
    }
}
