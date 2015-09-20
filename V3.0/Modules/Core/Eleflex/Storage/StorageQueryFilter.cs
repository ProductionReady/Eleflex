using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object specifying a storage query operation.
    /// </summary>
    public partial class StorageQueryFilter : IStorageQueryFilter
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public StorageQueryFilter()
        {
            Properties = new List<string>();
            Values = new List<string>();
        }

        /// <summary>
        /// The columns.
        /// </summary>
        public virtual IList<string> Properties { get; set; }

        /// <summary>
        /// The values.
        /// </summary>
        public virtual IList<string> Values { get; set; }

        /// <summary>
        /// The type of aggregate.
        /// </summary>
        public virtual StorageQueryAggregateType Aggregate { get; set; }

        /// <summary>
        /// The type of comparison.
        /// </summary>
        public virtual StorageQueryComparisonType Comparison { get; set; }

        /// <summary>
        /// The type of expression.
        /// </summary>
        public virtual StorageQueryExpressionType Expression { get; set; }

        /// <summary>
        /// The type of filter.
        /// </summary>
        public virtual StorageQueryFilterType FilterType { get; set; }

        /// <summary>
        /// The type of inclusion.
        /// </summary>
        public virtual StorageQueryInclusionType InclusionType { get; set; }

        /// <summary>
        /// The sort direction.
        /// </summary>
        public virtual bool SortAsc { get; set; }
    }
}
