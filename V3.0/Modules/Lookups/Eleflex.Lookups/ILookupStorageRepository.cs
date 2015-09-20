using System;
using System.Collections.Generic;

namespace Eleflex.Lookups
{
    /// <summary>
    /// Represents a Lookup storage repository.
    /// </summary>
    public partial interface ILookupStorageRepository : IStorageRepository<Lookup, System.Guid>
    {
    }
}

