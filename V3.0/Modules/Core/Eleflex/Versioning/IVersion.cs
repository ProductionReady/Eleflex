using System;

namespace Eleflex
{
    /// <summary>
    /// Represents an object defining a version in the patching process.
    /// </summary>
    public partial interface IVersion
    {
        /// <summary>
        /// Major. This is the major release of Eleflex. Currently V2.
        /// </summary>
        int Major { get; set; }
        /// <summary>
        /// Minor. This number increases when a storage change is required.
        /// </summary>
        int Minor { get; set; }
        /// <summary>
        /// Build. This number increases when an integrated component changes.
        /// </summary>
        int Build { get; set; }
        /// <summary>
        /// Revision. This number change when only a code change is required.
        /// </summary>
        int Revision { get; set; }
    }
}
