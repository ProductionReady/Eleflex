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
using System.Collections.Generic;
using Eleflex.Storage.Database.Filters;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a storage criteria filter used in querying and modifying storage data.
    /// </summary>
    public partial interface IPersistenceFilter
    {

        /// <summary>
        /// The enumeration type of the storage filter.
        /// </summary>
        FilterEnum FilterEnum { get; }

        /// <summary>
        /// Determines if used in the select clause.
        /// </summary>
        bool IsSelectClause { get; }

        /// <summary>
        /// Determines if used in the update clause.
        /// </summary>
        bool IsUpdateClause { get; }

        /// <summary>
        /// Determines if used in the from clause.
        /// </summary>
        bool IsFromClause { get; }        

        /// <summary>
        /// Determines if used in the where clause.
        /// </summary>
        bool IsWhereClause { get; }

        /// <summary>
        /// Determines if used in the sort clause
        /// </summary>
        bool IsSortClause { get; }

        /// <summary>
        /// Determines if used to group expressions.
        /// </summary>
        bool IsGroupingExpression { get; }
            
        /// <summary>
        /// Determines if used as a grouping start expression.
        /// </summary>
        bool IsGroupingStartExpression { get; }

        /// <summary>
        /// Determinies if is an expression.
        /// </summary>
        bool IsExpression { get; }        

        /// <summary>
        /// Determines if us used to join an expression.
        /// </summary>
        bool IsJoinExpression { get; }

        /// <summary>
        /// Determines if the filter is valid.
        /// </summary>
        bool IsValid { get; }        

        /// <summary>
        /// The list of properties associated to the filter.
        /// </summary>
        List<IEleflexProperty> Properties { get; }

        /// <summary>
        /// The list of data types associated to the filter.
        /// </summary>
        List<IEleflexDataType> Values { get; }
        
    }
}
