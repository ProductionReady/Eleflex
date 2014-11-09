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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines constants used for the service.
    /// </summary>
    public partial class SecurityConstants
    {        

        /// <summary>
        /// Default admin credentials.
        /// </summary>
        public static readonly SecurityCredentials Default_AdminSingleUserCredentials = new SecurityCredentials("Production", "Ready", "Eleflex");

        /// <summary>
        /// Default Guid value for admin
        /// </summary>
        public static Guid Default_AdminSecurityDomainID = new Guid("{00000000-0000-0000-0000-000000000000}");

        /// <summary>
        /// Default Guid value for admin
        /// </summary>
        public static Guid Default_AdminSecurityUserID = new Guid("{00000000-0000-0000-0000-000000000001}");

        /// <summary>
        /// Default Guid value for admin permission.
        /// </summary>
        public static Guid Default_AdminSecurityPermissionID = new Guid("{00000000-0000-0000-0000-000000000002}");

        /// <summary>
        /// Default Guid value for admin permission.
        /// </summary>
        public const string Default_AdminSecurityPermissionName = "admin";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_AuthenticationProviderNotDefined = "Authentication provider not defined";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_Unauthorized = "Unauthorized";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_SourceRestricted = "Source restricted";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_Action1 = "Action {0}";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_Restricted1 = "{0} restricted";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_DataNull1 = "{0} cannot be null";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_DomainOrUsernameOrPasswordInvalid = "Domain or username or password invalid";        

    }
}
