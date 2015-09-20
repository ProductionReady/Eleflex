using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object used for patching versions of modules within the system.
    /// </summary>
    public partial interface IModulePatch : IModule
    {
        
        /// <summary>
        /// Information relating to the patch.
        /// </summary>
        string PatchInfo { get; set; }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        List<Version> PriorVersions { get; }

        /// <summary>
        /// Dependent modules that must be executed prior to this version.
        /// </summary>
        List<Guid> DependentModules { get; }

        /// <summary>
        /// Custom logic to update the patch.
        /// </summary>
        /// <returns></returns>
        void Update();
        
    }
}
