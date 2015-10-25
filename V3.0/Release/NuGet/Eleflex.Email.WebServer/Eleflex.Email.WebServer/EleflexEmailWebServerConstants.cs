using System;

namespace Eleflex.Email.WebServer
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class EleflexEmailWebServerConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("1E31A858-1922-4D5B-8B03-4C69DD2D6871");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "Eleflex.Email.WebServer";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX library providing web server hosting for Email.";
    }
}
