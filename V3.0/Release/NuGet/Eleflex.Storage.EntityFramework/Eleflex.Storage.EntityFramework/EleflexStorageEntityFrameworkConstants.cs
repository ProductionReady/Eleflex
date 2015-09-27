using System;

namespace Eleflex.Storage.EntityFramework
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class EleflexStorageEntityFrameworkConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("DDAC21B7-E6FA-45EB-B1BF-84038A2AB2CC");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "Eleflex.Storage.EntityFramework";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX library providing data access using EntityFramework.";
    }
}
