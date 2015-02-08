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

namespace Eleflex.Security
{
    /// <summary>
    /// Constants for use with the security module.
    /// </summary>
    public class SecurityConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("8C727F8D-D891-4481-B879-D0926A18A53A");

        /// <summary>
        /// The module name for versioning.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Security Module";

        /// <summary>
        /// The module key for security storage providers.
        /// </summary>
        public static Guid STORAGE_PROVIDER_MODULE_KEY = Guid.Parse("72E85BF1-8903-45C2-8DD7-CA25F76DC08F");

        /// <summary>
        /// The name used to distinguish the security storage provider from others.
        /// </summary>
        public const string STORAGE_PROVIDER_NAME = "EleflexSecurityStorageProvider";

        /// <summary>
        /// RoleKey for a user.
        /// </summary>
        public static Guid ROLEKEY_USER = new Guid("0C3623CB-5643-4FCD-8B8C-949D66C51AF2");                
        /// <summary>
        /// RoleKey for an admin.
        /// </summary>
        public static Guid ROLEKEY_ADMIN = new Guid("202BBB57-1411-4FC5-BE1A-832520AB78E3");
        /// <summary>
        /// PermissionKey for an admin.
        /// </summary>
        public static Guid PERMISSIONKEY_ADMIN = new Guid("3E820CF7-4E24-4C56-8325-BF19ECB70CD7");
        /// <summary>
        /// PermissionKey for a user.
        /// </summary>
        public static Guid PERMISSIONKEY_USER = new Guid("A9C7918B-653C-4BFA-BF36-3C2CE2DC9489");

        /// <summary>
        /// Role for a user.
        /// </summary>
        public const string ROLE_USER = "User";
        /// <summary>
        /// Role for admin.
        /// </summary>
        public const string ROLE_ADMIN = "Admin";
        /// <summary>
        /// Permission for an admin.
        /// </summary>
        public const string PERMISSION_ADMIN = "Admin";
        /// <summary>
        /// Permission for a user.
        /// </summary>
        public const string PERMISSION_USER = "User";

    }
}
