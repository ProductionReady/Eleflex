#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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
using System.Runtime.Serialization;
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// Enumeration types of filters.
    /// </summary>
    public enum FilterEnum
    {

        /// <summary>
        /// Aggregate.
        /// </summary>
        Aggregate,
        /// <summary>
        /// Between.
        /// </summary>
        Between,
        /// <summary>
        /// Comparison.
        /// </summary>
        Compare,
        /// <summary>
        /// Distinct.
        /// </summary>
        Distinct,
        /// <summary>
        /// Expression.
        /// </summary>
        Expression,
        /// <summary>
        /// Relation.
        /// </summary>
        Relation,
        /// <summary>
        /// Like.
        /// </summary>
        Like,
        /// <summary>
        /// Not supported.
        /// </summary>
        NotSupported,
        /// <summary>
        /// Null.
        /// </summary>
        Null,
        /// <summary>
        /// PropertyComparison.
        /// </summary>
        PropertyCompare,
        /// <summary>
        /// Select.
        /// </summary>
        Select,
        /// <summary>
        /// Set.
        /// </summary>
        Set,
        /// <summary>
        /// Sort.
        /// </summary>
        Sort,
        /// <summary>
        /// Update.
        /// </summary>
        Update
    }
}
