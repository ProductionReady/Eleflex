using System;

namespace Eleflex.Lookups
{
    /// <summary>
    /// Static class containing constants for the Lookups module.
    /// </summary>
    public static partial class LookupsConstants
    {        

        /// <summary>
        /// The module key for Lookups.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("E3E389AE-5CEF-40D8-B271-13EFCCD30425");

        /// <summary>
        /// The module name for Lookups.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Lookups Module";

        /// <summary>
        /// The module key for Lookups.
        /// </summary>
        public static Guid STORAGE_SERVICE_MODULE_KEY = Guid.Parse("C51F23A4-2BDC-4971-9D3D-DF7C1355B436");

        /// <summary>
        /// The name used to distinguish the Lookups storage service from others.
        /// </summary>
        public const string STORAGE_SERVICE_NAME = "EleflexLookupsStorageService";
        
    }
}

