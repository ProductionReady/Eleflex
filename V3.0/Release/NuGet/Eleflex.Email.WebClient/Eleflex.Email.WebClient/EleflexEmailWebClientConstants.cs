using System;

namespace Eleflex.Email.WebClient
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class EleflexEmailWebClientConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("37A5F47C-B634-4177-B5E4-0FF3DB91879D");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "Eleflex.Email.WebClient";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX library providing web client hosting for Email.";

        /// <summary>
        /// 
        /// </summary>
        public const string EMAILIDENTITYMESSAGESERVICE_CONSTRUCTORPARAM_FROMEMAILADDRESS = "fromEmailAddress";
    }
}
