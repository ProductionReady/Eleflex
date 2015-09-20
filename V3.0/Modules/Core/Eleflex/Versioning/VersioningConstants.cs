using System;

namespace Eleflex
{
    /// <summary>
    /// Static class containing constants for the Versioning module.
    /// </summary>
    public static partial class VersioningConstants
    {        

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("F0C700E8-AEB6-4DBD-A813-A9F49D983A7F");
        
        /// <summary>
        /// The module name for versioning.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Versioning Module";

        /// <summary>
        /// The module key for versioning storage proviers.
        /// </summary>
        public static Guid STORAGE_SERVICE_MODULE_KEY = Guid.Parse("ECC9BCD6-5C0D-47B6-AF0D-72149664BB39");

        /// <summary>
        /// The name used to distinguish the versioning storage provider from others.
        /// </summary>
        public const string STORAGE_SERVICE_NAME = "EleflexVersioningStorageService";
        
    }
}

