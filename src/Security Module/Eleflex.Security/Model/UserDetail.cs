#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
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
            UserClaims = new List<UserClaim>();
        }
        /// <summary>
        /// Assigned Roles (and linked roles).
        /// </summary>
        public List<Role> Roles { get; protected set; }
        /// <summary>
        /// Assigned User Permissions (does not include role permissions)
        /// </summary>
        public List<Permission> UserPermissions { get; protected set; }

        /// <summary>
        /// Assigned User Claims
        /// </summary>
        public List<UserClaim> UserClaims { get; protected set; }
    }
}
