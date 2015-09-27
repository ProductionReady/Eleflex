using System;

namespace Eleflex.Lookups.WebServer
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class EleflexLookupsServerConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("E5B6BE42-11D2-40E3-8486-D8DB28768871");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "Eleflex.Lookups.Server";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX library providing data access and service hosting for Lookups.";
    }
}
