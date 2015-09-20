using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object containing instructions for querying storage objects.
    /// </summary>
    public partial class StorageQuery : IStorageQuery
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public StorageQuery()
        {
            StorageQueryFilters = new List<StorageQueryFilter>();
        }

        /// <summary>
        /// Collection of filters
        /// </summary>
        public virtual IList<StorageQueryFilter> StorageQueryFilters { get; set; }

        /// <summary>
        /// The start page.
        /// </summary>
        public virtual int PagingStartPage { get; set; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public virtual int PagingNumberPerPage { get; set; }

    }
}
