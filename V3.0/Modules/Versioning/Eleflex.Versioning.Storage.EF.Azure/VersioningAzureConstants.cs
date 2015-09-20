using System;

namespace Eleflex.Versioning.Storage.EF.Azure
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class VersioningAzureConstants
    {

        /// <summary>
        /// The module key.
        /// </summary>
        public static Guid MODULE_KEY = VersioningConstants.STORAGE_SERVICE_MODULE_KEY;

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "ELEFLEX Versioning Microsoft Azure";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX Versioning Microsoft Azure.";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string VERSIONING_STORAGE_CONFIG = MODULE_NAME;

    }
}
