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
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// Defines a storage criteria filter used in querying and modifying storage data.
    /// </summary>
    public abstract partial class FilterBase : IPersistenceFilter
    {

        /// <summary>
        /// Internal Properties.
        /// </summary>
        protected List<IEleflexProperty> _properties = new List<IEleflexProperty>();
        /// <summary>
        /// Internal Values.
        /// </summary>
        protected List<IEleflexDataType> _values = new List<IEleflexDataType>();
        /// <summary>
        /// Internal SupportedStorageFilter.
        /// </summary>
        protected FilterEnum _filterEnum;


        /// <summary>
        /// The enumeration type of the storage filter.
        /// </summary>
        public virtual FilterEnum FilterEnum
        {
            get { return _filterEnum; }
        }

        /// <summary>
        /// Determines if used in the select clause.
        /// </summary>
        public virtual bool IsSelectClause
        {
            get { return false; }
        }

        /// <summary>
        /// Determines if used in the update clause.
        /// </summary>
        public virtual bool IsUpdateClause
        {
            get { return false; }
        }        

        /// <summary>
        /// Determines if used in the from clause.
        /// </summary>
        public virtual bool IsFromClause
        {
            get { return false; }
        }

        /// <summary>
        /// Determines if used in the where clause.
        /// </summary>
        public virtual bool IsWhereClause
        {
            get { return false; }
        }

        /// <summary>
        /// Determines if used in the sort clause
        /// </summary>
        public virtual bool IsSortClause
        {
            get { return false; }
        }

        /// <summary>
        /// Determines if this expression is an and statement.
        /// </summary>
        public virtual bool IsAnd
        {
            get { return false; }
        }

        /// <summary>
        /// Determines if used to group expressions.
        /// </summary>
        public virtual bool IsGroupingExpression
        {
            get { return false; }
        }

        /// <summary>
        /// Determines if used as a grouping start expression.
        /// </summary>
        public virtual bool IsGroupingStartExpression
        {
            get { return false; }
        }

        /// <summary>
        /// Determinies if is an expression.
        /// </summary>
        public virtual bool IsExpression
        {
            get { return false; }
        }

        /// <summary>
        /// Determines if us used to join an expression.
        /// </summary>
        public virtual bool IsJoinExpression
        {
            get { return false; }
        }
        
        /// <summary>
        /// Determines if the filter is valid.
        /// </summary>
        public virtual bool IsValid
        {
            get { return true; }
        }

        /// <summary>
        /// The list of properties associated to the filter.
        /// </summary>
        public virtual List<IEleflexProperty> Properties
        {
            get { return _properties; }
            set { _properties = value; }
        }

        /// <summary>
        /// The list of data types associated to the filter.
        /// </summary>
        public virtual List<IEleflexDataType> Values
        {
            get { return _values; }
            set { _values = value; }
        }

    }
}
