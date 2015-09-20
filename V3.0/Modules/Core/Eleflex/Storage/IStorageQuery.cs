using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object containing instructions for querying storage objects.
    /// </summary>
    public partial interface IStorageQuery : IPaging
    {

        /// <summary>
        /// The collection of storage query filters.
        /// </summary>
        IList<StorageQueryFilter> StorageQueryFilters { get; set; }
    }
}
