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
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// Defines a property comparison filter item.
    /// </summary>
    public partial class FilterPropertyCompare : FilterBase
    {

        /// <summary>
        /// Internal ComparisonType.
        /// </summary>
        protected ComparisonEnum _comparisonType;


        /// <summary>
        /// Constructor.
        /// </summary>
        public FilterPropertyCompare() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="leftProperty"></param>
        /// <param name="comparisonType"></param>
        /// <param name="rightProperty"></param>
        public FilterPropertyCompare(IEleflexProperty leftProperty, ComparisonEnum comparisonType, IEleflexProperty rightProperty)
        {
            _properties.Add(leftProperty);
            _properties.Add(rightProperty);
            _comparisonType = comparisonType;
        }


        /// <summary>
        /// The comparison type.
        /// </summary>
        public virtual ComparisonEnum ComparisonEnum
        {
            get { return _comparisonType; }
            set { _comparisonType = value; }
        }

        /// <summary>
        /// The enumeration type of the storage filter.
        /// </summary>
        public override FilterEnum FilterEnum
        {
            get { return FilterEnum.PropertyCompare; }
        }

        /// <summary>
        /// Determines if used in the where clause.
        /// </summary>
        public override bool IsWhereClause
        {
            get { return true; }
        }

        /// <summary>
        /// Determines if the storage filter is valid.
        /// </summary>
        public override bool IsValid
        {
            get
            {
                if (_properties == null ||
                    _properties.Count != 2)
                {
                    return false;
                }
                return base.IsValid;
            }
        }
        
    }
}
