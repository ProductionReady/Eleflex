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
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// Defines a select filter item.
    /// </summary>
    public partial class FilterSelect : FilterBase
    {
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public FilterSelect() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="property"></param>
        public FilterSelect(IEleflexProperty property)
        {
            _properties.Add(property);
        }


        /// <summary>
        /// The enumeration type of the storage filter.
        /// </summary>
        public override FilterEnum FilterEnum
        {
            get { return FilterEnum.Select; }
        }

        /// <summary>
        /// Determines if used in the select clause.
        /// </summary>
        public override bool IsSelectClause
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
