using System;

namespace Eleflex.Server
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class EleflexServerConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("A1F7CB07-3576-469C-9FCC-8C8046511D92");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "Eleflex.Server";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX library providing data access and service hosting for core ELEFLEX services.";
    }
}
