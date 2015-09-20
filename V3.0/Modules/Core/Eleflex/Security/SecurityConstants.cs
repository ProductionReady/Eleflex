using System;

namespace Eleflex
{
    /// <summary>
    /// Static class containing constants for the Security module.
    /// </summary>
    public static partial class SecurityConstants
    {        

        /// <summary>
        /// The module key for Security.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("8C727F8D-D891-4481-B879-D0926A18A53A");

        /// <summary>
        /// The module name for Security.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Security Module";

        /// <summary>
        /// The module key for Security.
        /// </summary>
        public static Guid STORAGE_SERVICE_MODULE_KEY = Guid.Parse("72E85BF1-8903-45C2-8DD7-CA25F76DC08F");

        /// <summary>
        /// The name used to distinguish the Security storage service from others.
        /// </summary>
        public const string STORAGE_SERVICE_NAME = "EleflexSecurityStorageService";
        
    }
}

