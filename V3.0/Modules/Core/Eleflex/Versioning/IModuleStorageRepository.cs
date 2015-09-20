using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a Module storage repository.
    /// </summary>
    public partial interface IModuleStorageRepository : IStorageRepository<Module, System.Guid>
    {
    }
}

