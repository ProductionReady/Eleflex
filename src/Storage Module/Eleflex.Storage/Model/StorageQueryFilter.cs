#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;

namespace Eleflex.Storage
{
    /// <summary>
    /// Defines a filter to a storage query.
    /// </summary>
    public class StorageQueryFilter :IStorageQueryFilter
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public StorageQueryFilter()
        {
            Columns = new List<string>();
            Values = new List<string>();
        }

        /// <summary>
        /// Get/Set the Columns property.
        /// </summary>
        public virtual List<string> Columns { get; set; }

        /// <summary>
        /// Get/Set the Values property.
        /// </summary>
        public virtual List<string> Values { get; set; }

        /// <summary>
        /// Aggregate.
        /// </summary>
        public virtual StorageQueryAggregateType Aggregate { get; set; }

        /// <summary>
        /// Comparison.
        /// </summary>
        public virtual StorageQueryComparisonType Comparison { get; set; }

        /// <summary>
        /// Expression.
        /// </summary>
        public virtual StorageQueryExpressionType Expression { get; set; }

        /// <summary>
        /// Type of filter.
        /// </summary>
        public virtual StorageQueryFilterType FilterType { get; set; }

        /// <summary>
        /// Type of includion.
        /// </summary>
        public virtual StorageQueryInclusionType InclusionType { get; set; }

        /// <summary>
        /// Sort direction.
        /// </summary>
        public virtual bool SortAsc { get; set; }
    }
}
