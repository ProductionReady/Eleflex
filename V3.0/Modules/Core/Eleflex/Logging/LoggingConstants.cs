using System;

namespace Eleflex
{
    /// <summary>
    /// Static class containing constants for the Logging module.
    /// </summary>
    public static partial class LoggingConstants
    {        

        /// <summary>
        /// The module key for logging.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("FDA806E9-6BDC-481C-919D-E65B31DB89F3");

        /// <summary>
        /// The module name for logging.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Logging Module";

        /// <summary>
        /// The module key for logging storage provider.
        /// </summary>
        public static Guid STORAGE_SERVICE_MODULE_KEY = Guid.Parse("CD1B63E1-D585-40ED-A05E-EC88B75A7B6D");

        /// <summary>
        /// The name used to distinguish the logging storage provider from others.
        /// </summary>
        public const string STORAGE_SERVICE_NAME = "EleflexLoggingStorageService";  
        
    }
}

