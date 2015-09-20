using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object used to store system version infocmtion .
    /// </summary>
    public partial class PatchSystemSummary
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public PatchSystemSummary()
        {
            InstalledModules = new List<Module>();
            ModulePatches = new Dictionary<Guid, List<IModulePatch>>();
        }

        /// <summary>
        /// Collection of modules installed in the system already.
        /// </summary>
        public virtual IList<Module> InstalledModules { get; set; }

        /// <summary>
        /// Collection of patches for all embedded modules.
        /// </summary>
        public virtual IDictionary<Guid, List<IModulePatch>> ModulePatches { get; set; }
    }
}
