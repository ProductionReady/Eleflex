using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Patch for a module in the framework.
    /// </summary>
    public abstract partial class ModulePatch : Module, IModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="moduleKey"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="patchInfo"></param>
        public ModulePatch(Guid moduleKey, string name, string description, string patchInfo)
        {
            ModuleKey = moduleKey;
            Name = name;
            Description = description;
            PatchInfo = patchInfo;
        }

        /// <summary>
        /// Information relating to the patch.
        /// </summary>
        public virtual string PatchInfo { get; set; }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public abstract bool IsAvailable { get; }

        /// <summary>
        /// Dependent modules that must be executed prior to this version.
        /// </summary>
        public abstract List<Guid> DependentModules { get; }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        public abstract List<Version> PriorVersions { get; }
        
        /// <summary>
        /// Custom logic to update the patch.
        /// </summary>
        /// <returns></returns>
        public abstract void Update();

        /// <summary>
        /// Return patch name and version information quickly.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = Name + " " + ModuleKey.ToString();
            if (Version != null)
                output += " " + Version.ToString();
            return output;
        }

    }
}
