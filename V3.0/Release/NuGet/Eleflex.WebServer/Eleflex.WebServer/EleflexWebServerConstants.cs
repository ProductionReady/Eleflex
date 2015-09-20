using System;

namespace Eleflex.WebServer
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class EleflexWebServerConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("72C009B0-F05A-4A38-86DD-C46AE21AEFA5");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "ELEFLEX WebServer";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX WebServer library used for hosting a web server application.";
    }
}
