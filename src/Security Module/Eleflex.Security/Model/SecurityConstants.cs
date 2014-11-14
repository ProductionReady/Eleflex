#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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
        /// The name used to distinguish the security storage provider from others.
        /// </summary>
        public const string STORAGE_PROVIDER_NAME = "EleflexSecurityStorageProvider";

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("72E85BF1-8903-45C2-8DD7-CA25F76DC08F");

        /// <summary>
        /// The module name for versioning.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Security Module";

        /// <summary>
        /// RoleKey for all users.
        /// </summary>
        public static Guid ROLE_ALL_USERS = new Guid("0C3623CB-5643-4FCD-8B8C-949D66C51AF2");

        /// <summary>
        /// RoleKey for administrators.
        /// </summary>
        public static Guid ROLE_ADMINISTATORS = new Guid("202BBB57-1411-4FC5-BE1A-832520AB78E3");

        /// <summary>
        /// RoleKey for anonymous users.
        /// </summary>
        public static Guid ROLE_ANONYMOUS = new Guid("7D99ADF1-83E6-45E6-9EF0-48E958D82945");

    }
}
