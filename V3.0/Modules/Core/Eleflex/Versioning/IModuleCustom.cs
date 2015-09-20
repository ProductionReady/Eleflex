using System;

namespace Eleflex
{
    /// <summary>
    /// Represents an object containing a module's version information.
    /// </summary>
    public partial interface IModule
    {
        /// <summary>
        /// Unique Module key.
        /// </summary>
        Guid ModuleKey { get; set; }
        /// <summary>
        /// The name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The description.
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// Version installed.
        /// </summary>
        Version Version { get; set; }
    }
}
