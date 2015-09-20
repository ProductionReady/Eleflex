using System;

namespace Eleflex.Messages
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class EleflexMessagesConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("E04F6039-A67D-480E-98E7-A3280B05EE1D");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "ELEFLEX Messages";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX Messages library used for service communication.";
    }
}
