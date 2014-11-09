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
    /// Defines a null filter item.
    /// </summary>
    public partial class FilterNull : FilterBase
    {

        /// <summary>
        /// Internal IsNull.
        /// </summary>
        protected bool _isNull;


        /// <summary>
        /// Constructor.
        /// </summary>
        public FilterNull() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="isNull"></param>
        public FilterNull(IEleflexProperty property, bool isNull)
        {
            _properties.Add(property);
            _isNull = isNull;
        }

        /// <summary>
        /// Determines if inclusive nullability.
        /// </summary>
        public virtual bool IsNull
        {
            get { return _isNull; }
            set { _isNull = value; }
        }

        /// <summary>
        /// The enumeration type of the storage filter.
        /// </summary>
        public override FilterEnum FilterEnum
        {
            get { return FilterEnum.Null; }
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
                    _properties.Count != 1)
                {
                    return false;
                }
                return base.IsValid;
            }
        }
        
    }
}
