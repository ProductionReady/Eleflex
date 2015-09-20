using System;

namespace Eleflex.Security.Storage.EF.SQLServer
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class SecuritySQLServerConstants
    {

        /// <summary>
        /// The module key.
        /// </summary>
        public static Guid MODULE_KEY = SecurityConstants.STORAGE_SERVICE_MODULE_KEY;

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "ELEFLEX Security Microsoft SQL Server";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX Security Microsoft SQL Server.";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string VERSIONING_STORAGE_CONFIG = MODULE_NAME;
    }
}
