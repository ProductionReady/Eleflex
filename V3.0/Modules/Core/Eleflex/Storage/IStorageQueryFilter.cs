using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object specifying a storage query operation.
    /// </summary>
    public partial interface IStorageQueryFilter
    {

        /// <summary>
        /// The properties.
        /// </summary>
        IList<string> Properties { get; set; }

        /// <summary>
        /// The values.
        /// </summary>
        IList<string> Values { get; set; }

        /// <summary>
        /// The type of aggregate.
        /// </summary>
        StorageQueryAggregateType Aggregate { get; set; }

        /// <summary>
        /// The type of comparison.
        /// </summary>
        StorageQueryComparisonType Comparison { get; set; }

        /// <summary>
        /// The type of expression.
        /// </summary>
        StorageQueryExpressionType Expression { get; set; }

        /// <summary>
        /// The type of filter.
        /// </summary>
        StorageQueryFilterType FilterType { get; set; }

        /// <summary>
        /// The type of inclusion.
        /// </summary>
        StorageQueryInclusionType InclusionType { get; set; }

        /// <summary>
        /// The sort direction.
        /// </summary>
        bool SortAsc { get; set; }
    }
}
