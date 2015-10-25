using System;

namespace Eleflex.Email
{
    /// <summary>
    /// Static class containing constants for the Email module.
    /// </summary>
    public static partial class EmailConstants
    {        

        /// <summary>
        /// The module key for Email.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("7580E744-04CC-40CE-9E4F-9D8CFE5DE660");

        /// <summary>
        /// The module name for Email.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Email Module";

        /// <summary>
        /// The module key for Email.
        /// </summary>
        public static Guid STORAGE_SERVICE_MODULE_KEY = Guid.Parse("F4BD0416-BD8E-4583-86F7-5C0DD7F261D8");

        /// <summary>
        /// The name used to distinguish the Email storage service from others.
        /// </summary>
        public const string STORAGE_SERVICE_NAME = "EleflexEmailStorageService";
        
    }
}

