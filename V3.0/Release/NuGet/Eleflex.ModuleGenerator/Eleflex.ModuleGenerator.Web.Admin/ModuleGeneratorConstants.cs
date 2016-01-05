using System;

namespace Eleflex.ModuleGenerator.Web.Admin
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class ModuleGeneratorConstants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("9E8A17E1-38D6-40F8-988C-232F452A01AE");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = "Eleflex.ModuleGenerator";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = "ELEFLEX library providing module generation for developers.";
    }
}
