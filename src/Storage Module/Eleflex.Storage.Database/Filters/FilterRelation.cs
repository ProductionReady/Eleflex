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
    /// Defines a relationship filter item.
    /// </summary>
    public partial class FilterRelation : FilterBase
    {

        /// <summary>
        /// Internal Relationship.
        /// </summary>
        protected IPersistenceRelation _relation;


        /// <summary>
        /// Constructor.
        /// </summary>
        public FilterRelation() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="relation"></param>
        public FilterRelation(IPersistenceRelation relation)
        {
            _relation = relation;
            _properties.AddRange(relation.EPRGetParentProperties());
            _properties.AddRange(relation.EPRGetForeignProperties());
        }

        
        /// <summary>
        /// The relationship.
        /// </summary>
        public IPersistenceRelation Relationship
        {
            get { return _relation; }
        }

        /// <summary>
        /// The enumeration type of the storage filter.
        /// </summary>
        public override FilterEnum FilterEnum
        {
            get { return FilterEnum.Relation; }
        }

        /// <summary>
        /// Determines if used in the from clause.
        /// </summary>
        public override bool IsFromClause
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
                if (_relation == null)
                {
                    return false;
                }
                return base.IsValid;
            }
        }
        
    }
}
