using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a Module storage repository.
    /// </summary>
    public partial interface IModuleStorageRepository
    {
        /// <summary>
        /// Determine if repository is installed or not.
        /// </summary>
        bool IsInstalled();

        /// <summary>
        /// Get all module versions.
        /// </summary>
        /// <returns></returns>
        IList<Module> GetAll();
    }
}
